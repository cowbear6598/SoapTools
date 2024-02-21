using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MEC;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SoapTools.Addressable
{
    public class AddressableDownloadHandler
    {
        private int                     downloadCount;
        private ReactiveProperty<float> downloadProgress = new();
        private IList<string>           downloadLabels;

        private Action         OnAllDownloadCompleted;
        private Action<string> OnDownloadFailed;

        /// <summary>
        /// 初始化 Addressable 以及更新 Catalogs
        /// </summary>
        /// <exception cref="Exception">初始化資源失敗、檢查資源目錄失敗、更新資源目錄失敗</exception>
        public async UniTask Initialize()
        {
            // --- 初始化 Addressable ---
            await Addressables.InitializeAsync();

            // --- 檢查資源目錄 ---
            var checkCatalogHandle = Addressables.CheckForCatalogUpdates(false);

            await checkCatalogHandle;

            if (checkCatalogHandle.Status == AsyncOperationStatus.Failed)
            {
                Addressables.Release(checkCatalogHandle);
                throw new Exception("檢查資源目錄失敗");
            }

            var catalogs = checkCatalogHandle.Result;

            Addressables.Release(checkCatalogHandle);

            if (catalogs.Count == 0)
                return;

            // --- 更新資源目錄 ---
            var updateCatalogsHandle = Addressables.UpdateCatalogs(catalogs);

            await updateCatalogsHandle;

            if (updateCatalogsHandle.Status == AsyncOperationStatus.Failed)
            {
                throw new Exception("更新資源目錄失敗");
            }
        }

        /// <summary>
        /// 取得下載檔案大小
        /// </summary>
        /// <param name="labels">addressable key</param>
        /// <param name="sizeTag">要轉成甚麼樣的大小格式</param>
        /// <returns>檔案大小</returns>
        /// <exception cref="Exception">取得檔案大小失敗</exception>
        public async UniTask<float> GetDownloadSize(IList<string> labels, SizeTag sizeTag = SizeTag.MB)
        {
            long totalSize = 0;

            for (int i = 0; i < labels.Count; i++)
            {
                var handle = Addressables.GetDownloadSizeAsync(labels[i]);

                await handle;

                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    Addressables.Release(handle);
                    throw new Exception("取得檔案大小資訊失敗");
                }

                totalSize += handle.Result;
                Addressables.Release(handle);
            }

            float size = totalSize;

            for (int i = 0; i < (int)sizeTag; i++)
            {
                size /= 1024f;
            }

            return size;
        }

        /// <summary>
        /// 刪除資源
        /// </summary>
        public bool ClearAllData() => Caching.ClearCache();

        /// <summary>
        /// 開始下載 Addressable 檔案
        /// </summary>
        /// <param name="labels">要下載的 labels </param>
        /// <param name="OnAllDownloadCompleted">下載完成</param>
        /// <param name="OnDownloadFailed">下載失敗回傳 string</param>
        public void StartDownload(IList<string> labels, Action OnAllDownloadCompleted, Action<string> OnDownloadFailed)
        {
            downloadCount          = 0;
            downloadProgress.Value = 0;
            downloadLabels         = labels;

            this.OnAllDownloadCompleted = OnAllDownloadCompleted;
            this.OnDownloadFailed       = OnDownloadFailed;

            Timing.RunCoroutine(Download());
        }

        private IEnumerator<float> Download()
        {
            var handle = Addressables.DownloadDependenciesAsync(downloadLabels[downloadCount], true);

            handle.Completed += OnDownloadComplete;

            while (!handle.IsDone)
            {
                downloadProgress.Value = handle.PercentComplete * (1f / downloadLabels.Count) + (float)downloadCount / downloadLabels.Count;
                yield return Timing.WaitForOneFrame;
            }
        }

        private void OnDownloadComplete(AsyncOperationHandle handle)
        {
            if (handle.Status == AsyncOperationStatus.Failed)
            {
                OnDownloadFailed?.Invoke("下載失敗，請確認網路狀態並再試一次");
                return;
            }

            downloadCount++;
            downloadProgress.Value = (float)downloadCount / downloadLabels.Count;

            if (downloadCount == downloadLabels.Count)
            {
                OnAllDownloadCompleted?.Invoke();
                return;
            }

            Timing.RunCoroutine(Download());
        }

        /// <summary>
        /// 綁定下載進度
        /// </summary>
        public IDisposable SubscribeDownloadProgress(Action<float> onValueChanged)
            => downloadProgress.Subscribe(onValueChanged);
    }
}
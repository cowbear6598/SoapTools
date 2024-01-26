using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SoapTools.Addressable
{
    public class AddressableDownloadHandler
    {
        private int                     downloadCount;
        private ReactiveProperty<float> downloadProgress = new();

        /// <summary>
        /// 初始化 Addressable 以及更新 Catalogs
        /// </summary>
        /// <exception cref="Exception">初始化資源失敗、檢查資源目錄失敗、更新資源目錄失敗</exception>
        public async UniTask Initialize()
        {
            // --- 初始化 Addressable ---
            var initializeHandle = Addressables.InitializeAsync();

            await initializeHandle;

            if (initializeHandle.Status != AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(initializeHandle);
                throw new Exception("初始化資源失敗");
            }

            Addressables.Release(initializeHandle);

            // --- 檢查資源目錄 ---
            var checkCatalogHandle = Addressables.CheckForCatalogUpdates(false);

            await checkCatalogHandle;

            if (checkCatalogHandle.Status != AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(checkCatalogHandle);
                throw new Exception("檢查資源目錄失敗");
            }

            var catalogs = checkCatalogHandle.Result;
            Addressables.Release(checkCatalogHandle);

            if (catalogs.Count == 0)
                return;

            // --- 更新資源目錄 ---
            var updateCatalogsHandle = Addressables.UpdateCatalogs(catalogs, false);

            await updateCatalogsHandle;

            if (updateCatalogsHandle.Status != AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(updateCatalogsHandle);
                throw new Exception("更新資源目錄失敗");
            }

            Addressables.Release(updateCatalogsHandle);
        }

        public async UniTask<long> GetDownloadSize(IList<string> labels)
        {
            return 0;
            for (int i = 0; i < labels.Count; i++) { }
        }

        /// <summary>
        /// 綁定下載進度
        /// </summary>
        public IDisposable SubscribeDownloadProgress(Action<float> onValueChanged)
            => downloadProgress.Subscribe(onValueChanged);
    }
}
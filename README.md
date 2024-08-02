# Document

- 自己在開發時基本上都會用到的工具或功能，主要想保有實作的多變性，所以很多其實還得多寫一點，例如 SceneController，可以用 DI 直接解決一堆問題，也可以用純粹的寫法搞定，所以就看使用的人怎麼做。
- 依賴：
    - [UniRx](https://github.com/neuecc/UniRx)
    - [UniTask](https://github.com/Cysharp/UniTask)
    - Addressable
    - [MEC (More Effective Coroutines)](https://assetstore.unity.com/packages/tools/animation/more-effective-coroutines-free-54975) (已放入在裡面)

# Install

```
Name: package.openupm.com
URL: https://package.openupm.com
Scope(s): 
         com.cysharp.unitask
         com.neuecc.unirx
```

- Open Window => Package Manager
- Add package from git URL

```
https://github.com/cowbear6598/SoapTools.git?path=Assets/SoapTools
```

# 功能介紹

## 群體改名

- Open Soap => 物件改名
- 選擇同樣階層的物件們，給他前墜與後墜以及起始編號，就可以一次改完所有物件的名稱。

## 更改 Sprite Order

- Open Soap => 設定 Sprite Order
- 偵測物件的 y 軸，並且依照 y 軸的大小以及間距來設定 Sprite Order。

## 2D 自適應

- AutoResolutionRect2D.cs，藉由更改 Camera 的 Rect 來達到自適應的效果。
- 實現與邏輯是源自 [此處](http://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html)。
- 程式碼的攝影機目前是抓由 Camera.main 來取得，可以自行修改，如有需要可以自行複製程式碼更改。
- 由於是更改攝影機的 Rect，所以勢必需要增加一個低效能的攝影機來處理背景或者想直接留黑也可以，看個人需求。

## UI 自適應

- CanvasResolutionHandler.cs 放在有 CanvasScaler 的物件上，可以自動調整要以寬或高為基準保持 UI 大小與位置。

## Web Camera

- PhysicCameraHandler.cs 使用 EnableCamera() 來開啟後相機。
- GetWebCamTexture() 可以取得相機的 Texture。
- GetWetCamCorrectInfo() 可以取得相機的 aspect、rotation、scale 等資訊來矯正顯示問題，實際使用方法可以參考 UI_WebCam.cs。
- DisableCamera() 關閉相機。
- PauseCamera() 暫停相機。

## Addressable (Preview)

- 目前只有簡易的下載與熱更新。

## 資料庫

- 分別為 Restful 以及 GraphQL。
- 可以藉由建立 ScriptableObject 來呼叫 Api，Create => Soap => Database => 選擇要用的方法。
- 也可以使用 RestfulBuilder 或 GraphQLBuilder 來建立 UnityWebRequest，以下是通用的功能。
    - SetUrl(string url) - 設定網址。
    - SetTimeout(int timeout) - 設定超時時間。
    - AddHeader(string key, string value) - 增加 Header。
    - Build() - 建立 UnityWebRequest。
    - StartRequest<TResponseData>() - 執行 UnityWebRequest，返回 UniTask<TResponseData>。

### Restful

- ScriptableObject
    - ![](https://github.com/cowbear6598/SoapTools/blob/main/Screenshots/RestfulSO.png)

- RestfulBuilder
    - SetRequestSO(RestfulRequestSO requestSO) - 設定 ScriptableObject。
    - SetMethod(RestfulMethod method) - 設定方法。
    - SetBody<TBody>(TBody body) - 設定 Body。
    - AddQuery(string key, string value) - 增加 Query。

## 場景轉換 (Alpha)

- 詳細可看 SceneTransition 場景，如果有 DI Framework，很多東西可以變得很簡單就注入。
- 使用方法：
    - ISceneTransition 建立自己的場景轉換效果並套上此 Interface 讓系統使用。
    - SceneRepository 用來管理場景。
    - SceneControllerBuilder(SceneRepository, ISceneTransition?) 用來建立場景轉換的流程，也是使用場景轉換的進入點，ISceneTransition 為可選。
        - TransitionIn - 開始轉換
        - TransitionOut - 結束轉換
        - LoadScene(AssetReference) - 載入指定場景
        - UnloadLastScene - 卸載最後一個場景
        - UnloadAllScene - 卸載全部場景

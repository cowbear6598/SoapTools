# Document

- 自己在開發時基本上都會用到的工具或功能，基本上得依賴於 DI 架構，例如：Zenject、VContainer。

# Install

- Open Edit => Porject Settings => Package Manager
- Add a new Scoped Registry

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

### GraphQL

- ScriptableObject
    - ![](https://github.com/cowbear6598/SoapTools/blob/main/Screenshots/GraphQLSO.png)
- GraphQLBuilder
    - SetRequestSO(GraphQLRequestSO requestSO) - 設定 ScriptableObject。
    - SetOperation(Operation operation) - 設定 query/mutation。
    - SetContent(string content) - 設定內容。

# 小物件功能

- CanvasResolutionHandler.cs 放在有 CanvasScaler 的物件上，可以自動調整要以寬或高為基準保持 UI 大小與位置。

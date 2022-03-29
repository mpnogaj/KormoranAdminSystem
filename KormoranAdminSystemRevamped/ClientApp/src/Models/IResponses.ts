interface IBasicResponse {
    message: string,
    error: boolean;
}

interface ICollectionResponse<T> extends IBasicResponse {
    collection: Array<T>;
}

interface ILoginResponse extends IBasicResponse {
    sessionId: string;
}

interface ISingleItemResponse<T> extends IBasicResponse {
    data: T;
}

export {
    IBasicResponse,
    ICollectionResponse,
    ISingleItemResponse,
    ILoginResponse
}
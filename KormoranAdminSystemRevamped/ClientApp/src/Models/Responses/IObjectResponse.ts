import IBasicResponse from "./IBasicResponse";

interface IObjectResponse<T> extends IBasicResponse{
    item: T;
}

export default IObjectResponse;
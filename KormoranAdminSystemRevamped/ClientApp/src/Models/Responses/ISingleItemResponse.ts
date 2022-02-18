import IBasicResponse from "./IBasicResponse";

interface ISingleItemResponse<T> extends IBasicResponse{
    data: T;
}

export default ISingleItemResponse;
import IBasicResponse from "./IBasicResponse";

interface ICollectionResponse<T> extends IBasicResponse {
	collection: Array<T>;
}

export default ICollectionResponse
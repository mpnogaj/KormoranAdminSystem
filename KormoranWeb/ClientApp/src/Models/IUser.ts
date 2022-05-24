interface IUser {
	id: number,
	login: string,
	fullname: string,
	isAdmin: boolean
}

const DEFAULT_USER: IUser = {
	id: 0,
	login: "",
	fullname: "",
	isAdmin: false,
};

export {
	DEFAULT_USER
};

export default IUser;
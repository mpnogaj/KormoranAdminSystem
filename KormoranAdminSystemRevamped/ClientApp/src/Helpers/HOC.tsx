import React, { ReactElement } from "react";
import { Params, useParams, useNavigate, NavigateFunction } from "react-router";

interface IWithParams {
		params: Readonly<Params<string>>;
}

export function withParams<T extends IWithParams>(Component: React.ComponentType<T>): (props: T) => JSX.Element{
	const componentWithParams = (props: T): ReactElement<T> => {
		const params = useParams();
		return (<Component {...props} params={params}/>);
	};
	return componentWithParams;
}

interface IWithNavigaton{
	navigation: NavigateFunction
}

export function withNavigation<T extends IWithNavigaton>(Component: React.ComponentType<T>): (props: T) => JSX.Element{
	const componentWithParams = (props: T): ReactElement<T> => {
		const navigation = useNavigate();
		return (<Component {...props} navigation={navigation}/>);
	};
	return componentWithParams;
}
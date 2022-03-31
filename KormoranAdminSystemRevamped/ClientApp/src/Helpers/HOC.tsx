import React, { ComponentType, ReactElement } from "react";
import { Params, useParams, useNavigate, NavigateFunction } from "react-router";

interface IWithParams {
	params: Readonly<Params<string>>;
}

export function withParams<T extends IWithParams>(Component: React.ComponentType<T>): (props: T) => JSX.Element {
	const componentWithParams = (props: T): ReactElement<T> => {
		const params = useParams();
		return (<Component {...props} params={params} />);
	};
	return componentWithParams;
}

export interface IWithNavigaton {
	navigation: NavigateFunction
}

export function withNavigation<P extends IWithNavigaton>(WrappedComponent: ComponentType<P>): ComponentType<Omit<P, keyof IWithNavigaton>> {
	const comp = (props: object): JSX.Element => {
		const nav = useNavigate();
		return (<WrappedComponent navigation={nav} {...props as any}/>);
	};
	return comp;
}

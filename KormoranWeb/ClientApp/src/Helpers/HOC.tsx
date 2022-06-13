import React, { ComponentType } from "react";
import { Params, useParams, useNavigate, NavigateFunction } from "react-router";

interface IWithParams {
    params: Readonly<Params<string>>;
}

export function WithParams<P extends IWithParams>(WrappedComponent: ComponentType<P>): ComponentType<Omit<P, keyof IWithParams>> {
	const Comp = (props: object): JSX.Element => {
		const params = useParams();
		return (<WrappedComponent params={params} {...props as any} />);
	};
	return Comp;
}

export interface IWithNavigaton {
    navigation: NavigateFunction
}

export function WithNavigation<P extends IWithNavigaton>(WrappedComponent: ComponentType<P>): ComponentType<Omit<P, keyof IWithNavigaton>> {
	const Comp = (props: object): JSX.Element => {
		const nav = useNavigate();
		return (<WrappedComponent navigation={nav} {...props as any} />);
	};
	return Comp;
}
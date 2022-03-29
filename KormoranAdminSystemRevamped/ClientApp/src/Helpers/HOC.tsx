import React from "react";
import { useParams } from "react-router";

export default function withParams(Component: new () => React.Component<any, any>): any {
	const comp = returnWithParams(Component, {});
	return comp;
}

function returnWithParams(Component: new () => React.Component<any, any>, props: any): JSX.Element {
	const params = useParams();
	return (
		<Component {...props} params={params} />
	);
}
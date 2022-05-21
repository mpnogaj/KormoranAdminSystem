import { nanoid } from "nanoid";
import React from "react";

export interface ISelectElement {
    id: number,
    name: string
}

interface IProps {
    addNullElement: boolean,
    header: string,
    items: Array<ISelectElement>,
    selection: number,
    onSelectionChanged: (newId: number) => void
}

interface IState {
    selection: number
}

class SelectBox extends React.Component<IProps, IState>{
	readonly selectBoxId: string;

	constructor(props: IProps) {
		super(props);
		this.state = {
			selection: props.selection
		};
		this.selectBoxId = "selectBox-" + nanoid();
	}

	renderList = (): Array<JSX.Element> => {
		const list: Array<JSX.Element> = [];
		if (this.props.addNullElement) {
			list.push(
				<option key={0} value={0}>
                    -
				</option>
			);
		}
		this.props.items.forEach((item) => {
			list.push(
				<option key={item.id} value={item.id}>
					{item.name}
				</option>
			);
		});
		return list;
	};

	render(): JSX.Element {
		return (
			<div className="mt-3 flexbox inline-d">
				<label className="me-3" htmlFor={this.selectBoxId}>{this.props.header}: </label>
				<select id={this.selectBoxId} value={this.state.selection}
					onChange={(event): void => {
						const newId = parseInt(event.target.value);
						this.props.onSelectionChanged(newId);
						this.setState({
							selection: newId
						});
					}}>
					{this.renderList()}
				</select>
			</div>
		);
	}
}

export default SelectBox;
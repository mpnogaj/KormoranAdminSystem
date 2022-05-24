import React from "react";
import { Badge } from "react-bootstrap";
import { Pencil, Trash } from "react-bootstrap-icons";
import IUser from "../Models/IUser";
import IconButton from "./IconButton";

interface IProps {
	user: IUser,
	showEdit: boolean,
	onEditClicked: () => void,
	onDeleteClicked: () => void
}

class UserRow extends React.Component<IProps> {
	render(): JSX.Element {
		return (
			<tr>
				<td>{this.props.user.id}</td>
				<td>{this.props.user.login}</td>
				<td>{this.props.user.fullname}</td>
				<td>{
					this.props.user.isAdmin
						? <Badge bg="warning">Administrator</Badge>
						: <Badge bg="success">Sędzia</Badge>
				}</td>
				{
					this.props.showEdit
						?
						<td>
							<div>
								<IconButton icon={<Pencil height={24} width={24} />} onClick={this.props.onEditClicked} />
								<IconButton icon={<Trash height={24} width={24} />} onClick={this.props.onDeleteClicked} />
							</div>
						</td>
						:
						null
				}
			</tr>
		);
	}
}

export default UserRow;
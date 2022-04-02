import React from "react";
import IMatch from "../Models/IMatch";
import { Table } from "react-bootstrap";
import MatchesRow from "./MatchRow";
interface IProps {
	matches: Array<IMatch>
}

class MatchesTable extends React.Component<IProps>{
	render(): JSX.Element {
		return (
			<div className="table-responsive">
				<Table hover={true} bordered={true}>
					<thead>
						{
							<tr>
								<th>L.p.</th>
								<th>Status</th>
								<th>Drużyna 1</th>
								<th>Drużyna 2</th>
								<th>Zwycięzca</th>
								<th>Wynik</th>
							</tr>
						}
					</thead>
					<tbody className="align-middle">
						{
							this.props.matches.map((val: IMatch): JSX.Element => {
								return <MatchesRow key={val.matchId} match={val} />;
							})
						}
					</tbody>
				</Table>
			</div>
		);
	}
}

export default MatchesTable;
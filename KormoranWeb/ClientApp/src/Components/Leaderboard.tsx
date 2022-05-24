import ILeaderboardEntry from "../Models/ILeaderboardEntry";
import React from "react";
import { Table } from "react-bootstrap";

interface IProps {
    leaderboard: Array<ILeaderboardEntry>
}

class Leaderboard extends React.Component<IProps>{
	renderLeaderboards = (): Array<JSX.Element> => {
		let place = 1;
		return this.props.leaderboard.map((entry, i): JSX.Element => {
			if (i > 0 && this.props.leaderboard[i - 1].wins > entry.wins) {
				place++;
			}
			return (
				<tr key={i}>
					<td>{place}</td>
					<td>{entry.team.name}</td>
					<td>{entry.wins}</td>
				</tr>
			);
		});
	};

	render(): JSX.Element {
		return (
			<Table responsive bordered hover>
				<thead>
					<tr>
						<td>Miejsce</td>
						<td>Drużyna</td>
						<td>Zwycięztwa</td>
					</tr>
				</thead>
				<tbody>
					{this.renderLeaderboards()}
				</tbody>
			</Table>
		);
	}
}

export default Leaderboard;
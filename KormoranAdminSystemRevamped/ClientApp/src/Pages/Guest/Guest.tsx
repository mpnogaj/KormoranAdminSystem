import React from "react";

class Guest extends React.Component{

	render(){
		return (
			<div className="container mt-3">
			<div className="logo-container">
				<p>Podgląd turniejów na żywo</p>
			</div>
			<table className="table table-hover table-bordered">
				<thead>
					<tr>
						<th>Nazwa</th>
						<th>Status</th>
						<th>Dyscyplina</th>
						<th>Typ turnieju</th>
						<th>Akcja</th>
					</tr>
				</thead>
				<tbody className="align-middle">
					<tr>
						<td>Testowa turbo uber giga długa long long nazwa przykładowego giga chad turnieju kormoran beach party</td>
						<td>
							<span className="badge bg-success">Aktywny</span>
						</td>
						<td>Piłka nożna</td>
						<td>Round Robin</td>
						<td>
							<a href="#" className="btn btn-success">Podgląd</a>
						</td>
					</tr>
					<tr>
						<td>Testowa turbo uber giga długa long long nazwa przykładowego giga chad turnieju kormoran beach party</td>
						<td>
							<span className="badge bg-success">Aktywny</span>
						</td>
						<td>Piłka nożna</td>
						<td>Round Robin</td>
						<td>
							<a href="#" className="btn btn-success">Podgląd</a>
						</td>
					</tr>
				</tbody>
			</table>
		</div>
		);
	}
}

export default Guest;
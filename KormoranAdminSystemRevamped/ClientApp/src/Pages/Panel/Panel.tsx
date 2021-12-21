import React from "react";
import "./Panel.css";
import {Col, Container, Row} from "react-bootstrap";
import {ReactComponent as Logo} from "../../Icons/LogoNoText.svg";
import {ReactComponent as Avatar} from "../../Icons/DefaultAvatar.svg";

class Panel extends React.Component<any, any>{
	render() {
		return (
			<div id="panelRoot">
				<nav className="navbar sticky-top navbar-expand-xl navbar-light bg-light">
					<Container id="navBar">
						<a className="navbar-brand" href="https://tools.webdevpuneet.com/">
							<div className="d-inline">
								<Logo height={52} width={100}/>
								<span className="h5">Kormoran Admin System</span>
							</div>
						</a>
						<button className="navbar-toggler collapsed" type="button" data-bs-toggle="collapse"
					        data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
					        aria-expanded="false" aria-label="Toggle navigation">
							<span className="navbar-toggler-icon"/>
						</button>
						<div className="navbar-collapse collapse" id="navbarSupportedContent">
							<ul className="navbar-nav me-auto mb-2 mb-lg-0">
								<hr className="fatHr"/>
								<li className="nav-item">
									<div className="navbar-nav dropdown me-auto mb-2 mb-lg-0">
										<a className="nav-link dropdown-toggle" href="#" id="userDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
											<div className="d-inline">
												<Avatar height={50} width={50}/>
												<span className="ms-3 h5">Marcin Nogaj</span>
											</div>
										</a>
										<ul className="dropdown-menu" aria-labelledby="userDropdown">
											<li><a className="dropdown-item" href="#">Twoje konto</a></li>
											<li><a className="dropdown-item" href="#">Ustawienia</a></li>
											<li><hr className="dropdown-divider"/></li>
											<li><a className="dropdown-item" href="#">Wyloguj</a></li>
										</ul>
									</div>
								</li>
								<hr/>
								<li className="nav-item">
									<a className="nav-link" aria-current="page" href="#">Panel</a>
								</li>
								<li className="nav-item">
									<a className="nav-link" href="#">Dziennik zdarzeń</a>
								</li>
								<li className="nav-item">
									<a className="nav-link" href="#">Operacje na turniejach</a>
								</li>
								<li className="nav-item">
									<a className="nav-link" href="#">Operacje na użytkownikach</a>
								</li>
								<li className="nav-item">
									<a className="nav-link" href="/Guest">Podgląd LIVE</a>
								</li>
							</ul>
						</div>
					</Container>
				</nav>
				<Container fluid style={{background: "red"}}>
					<Row style={{width: "100%"}}>
						<Col>
							<p>Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 </p>
							<p>Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 </p>
							<p>Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 </p>
							<p>Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 Dupa dupa 321 </p>
						</Col>
					</Row>
				</Container>
			</div>
		)
	}
}

export default Panel;
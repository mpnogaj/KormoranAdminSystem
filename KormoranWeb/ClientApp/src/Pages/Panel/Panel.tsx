import React from "react";
import "./Panel.css";
import { Container } from "react-bootstrap";
import { ReactComponent as Logo } from "../../Icons/LogoNoText.svg";
import { ReactComponent as Avatar } from "../../Icons/DefaultAvatar.svg";
import { Speedometer2, JournalText, PersonCheck, Tv, Gear } from "react-bootstrap-icons";
import axios from "axios";
import CookieManager from "../../Helpers/CookieManager";
import { LOG_OUT } from "../../Helpers/Endpoints";

interface IProps {
  content: JSX.Element;
}

class Panel extends React.Component<IProps>{
	render(): JSX.Element {
		return (
			<div>
				<nav className="navbar sticky-top navbar-expand-xl navbar-light bg-light">
					<Container id="navBar">
						<a className="navbar-brand">
							<div className="d-inline">
								<Logo height={52} width={94} />
								<span className="h5 align-middle">Kormoran Admin System</span>
							</div>
						</a>
						<button className="navbar-toggler collapsed" type="button" data-bs-toggle="collapse"
							data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
							aria-expanded="false" aria-label="Toggle navigation">
							<span className="navbar-toggler-icon" />
						</button>
						<div className="navbar-collapse collapse" id="navbarSupportedContent">
							<ul className="navbar-nav me-auto mb-2 mb-lg-0">
								<hr className="fatHr" />
								<li className="nav-item">
									<div className="dropdown me-auto mb-2 mb-lg-0">
										<a className="nav-link dropdown-toggle" href="#" id="userDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
											<div className="d-inline">
												<Avatar height={50} width={50} />
												<span className="ms-3 h5">{CookieManager.getCookie("fullname")}</span>
											</div>
										</a>
										<ul className="dropdown-menu" aria-labelledby="userDropdown">
											<li><a className="dropdown-item" href="#">Twoje konto</a>
											</li>
											<li><a className="dropdown-item" href="#">Ustawienia</a></li>
											<li><hr className="dropdown-divider" /></li>
											<li><a className="dropdown-item" onClick={async (): Promise<void> => {
												const res = await axios.post(LOG_OUT);
												console.log(res);
												window.location.href = "/Login";
											}}>Wyloguj</a></li>
										</ul>
									</div>
								</li>
								<hr />
								<li className="nav-item">
									<a className="nav-link" href="/Panel/Overview">
										<div className="d-inline">
											<Speedometer2 size={25} />
											<span className="ms-2 h5 align-middle">Przegląd</span>
										</div>
									</a>
								</li>
								<li className="nav-item">
									<a className="nav-link" href="/Panel/Logs">
										<div className="d-inline">
											<JournalText size={25} />
											<span className="ms-2 h5 align-middle">Dziennik zdarzeń</span>
										</div>
									</a>
								</li>
								<li className="nav-item">
									<a className="nav-link" href="/Panel/Tournaments">
										<div className="d-inline">
											<Gear size={25} />
											<span className="ms-2 h5 align-middle">Operacje na turniejach</span>
										</div>
									</a>
								</li>
								<li className="nav-item">
									<a className="nav-link" href="/Panel/Users">
										<div className="d-inline">
											<PersonCheck size={25} />
											<span className="ms-2 h5 align-middle">Operacje na użytkownikach</span>
										</div>
									</a>
								</li>
								<li />
								<li className="nav-item">
									<a className="nav-link" href="/Guest">
										<div className="d-inline">
											<Tv size={25} />
											<span className="ms-2 h5 align-middle">Podgląd LIVE</span>
										</div>
									</a>
								</li>
							</ul>
						</div>
					</Container>
				</nav>
				<div id="container">{this.props.content}</div>
			</div>
		);
	}
}

export default Panel;
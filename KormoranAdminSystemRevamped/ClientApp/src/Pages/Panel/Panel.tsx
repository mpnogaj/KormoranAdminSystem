import React from "react";
import "./Panel.css";
import {Container} from "react-bootstrap";
import {ReactComponent as Logo} from "../../Icons/LogoNoText.svg";
import {ReactComponent as Avatar} from "../../Icons/DefaultAvatar.svg";
import {Speedometer2, JournalText, PersonCheck, Tv, Gear} from "react-bootstrap-icons";
import axios from "axios";
import IDisciplinesResponse from "../../Models/Responses/IDisciplinesResponse";

interface IState{
	currentUrl: string,
	isLoading: boolean;
}

class Panel extends React.Component<any, IState>{
	
	constructor(props: any) {
		super(props);
		this.state = {
			currentUrl: "/Panel/Overview",
			isLoading: true
		}
		this.downloadDisciplines().catch(ex => console.error(ex));
	}
	
	changeUrl = (newUrl: string) => {
		if(newUrl != this.state.currentUrl){
			this.setState({currentUrl: newUrl, isLoading: true})
		}
	};
	
	downloadDisciplines = async () => {
		if(sessionStorage.getItem("disciplines") != null) return;
		const response = await axios.get<IDisciplinesResponse>("/api/");
		sessionStorage.setItem("disciplines", JSON.stringify(response.data.disciplines));
	}
	
	downloadStates = async () => {
		if(sessionStorage.getItem("states") != null) return;
		
	}
	
	render() {
		return (
			<div>
				<nav className="navbar sticky-top navbar-expand-xl navbar-light bg-light">
					<Container id="navBar">
						<a className="navbar-brand" href="https://tools.webdevpuneet.com/">
							<div className="d-inline">
								<Logo height={52} width={94}/>
								<span className="h5 align-middle">Kormoran Admin System</span>
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
									<div className="dropdown me-auto mb-2 mb-lg-0">
										<a className="nav-link dropdown-toggle" href="#" id="userDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
											<div className="d-inline">
												<Avatar height={50} width={50}/>
												<span className="ms-3 h5">Marcin Nogaj</span>
											</div>
										</a>
										<ul className="dropdown-menu" aria-labelledby="userDropdown">
											<li><a className="dropdown-item" href="#">Twoje konto</a>
											</li>
											<li><a className="dropdown-item" href="#">Ustawienia</a></li>
											<li><hr className="dropdown-divider"/></li>
											<li><a className="dropdown-item" onClick={async () => {
												await axios.post("/api/Session/Logout", {
													sessionId: sessionStorage.getItem("sessionId")
												});
												sessionStorage.removeItem("sessionId");
												window.location.href = "/Login";
											}}>Wyloguj</a></li>
										</ul>
									</div>
								</li>
								<hr/>
								<li className="nav-item">
									<a className="nav-link" data-bs-toggle="collapse"
									   data-bs-target=".navbar-collapse.show" href="#" 
									   onClick={() => this.changeUrl("/Panel/Overview")}>
										<div className="d-inline">
											<Speedometer2 size={25}/>
											<span className="ms-2 h5 align-middle">Przegląd</span>
										</div>
									</a>
								</li>
								<li className="nav-item">
									<a className="nav-link" data-bs-toggle="collapse" 
									   data-bs-target=".navbar-collapse.show" href="#" 
									   onClick={() => this.changeUrl("/Panel/Logs")}>
										<div className="d-inline">
											<JournalText size={25}/>
											<span className="ms-2 h5 align-middle">Dziennik zdarzeń</span>
										</div>
									</a>
								</li>
								<li className="nav-item">
									<a className="nav-link" data-bs-toggle="collapse"
									   data-bs-target=".navbar-collapse.show"
									   href="#" onClick={() => this.changeUrl("/Panel/Tournaments")}>
										<div className="d-inline">
											<Gear size={25}/>
											<span className="ms-2 h5 align-middle">Operacje na turniejach</span>
										</div>
									</a>
								</li>
								<li className="nav-item">
									<a className="nav-link" data-bs-toggle="collapse"
									   data-bs-target=".navbar-collapse.show"
									   href="#" onClick={() => this.changeUrl("/Panel/Users")}>
										<div className="d-inline">
											<PersonCheck size={25}/>
											<span className="ms-2 h5 align-middle">Operacje na użytkownikach</span>
										</div>
									</a>
								</li>
								<li/>
								<li className="nav-item">
									<a className="nav-link" href="/Guest">
										<div className="d-inline">
											<Tv size={25}/>
											<span className="ms-2 h5 align-middle">Podgląd LIVE</span>
										</div>
									</a>
								</li>
							</ul>
						</div>
					</Container>
				</nav>
				<iframe src={this.state.currentUrl} frameBorder={0} id="panelIframe"
				        style={{display: this.state.isLoading ? "none" : "block"}} 
				        onLoad={(() => this.setState({isLoading: false}))}/>
			</div>
		)
	}
}

export default Panel;
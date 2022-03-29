import React from "react";
import { Button, Col, Container, FormControl, FormFloating, Row } from 'react-bootstrap'
import axios from "axios";
import './Login.css';
import { ILoginResponse } from "../../Models/IResponses";
import { useNavigate } from "react-router";

interface IState {
	username: string,
	password: string,
	buttonEnabled: boolean,
	errorText: string;
}

class Login extends React.Component<any, IState>{

	constructor(props: any) {
		super(props);
		this.state = {
			username: "",
			password: "",
			buttonEnabled: true,
			errorText: ""
		};
	}

	loginClick = (e: React.FormEvent) => {
		this.loginHandler(e).then(correct => {
			if (correct) {
				this.props.navigation("/Panel");
			}
		})
	}

	loginHandler = async (e: React.FormEvent) => {
		this.setState<"buttonEnabled">({ buttonEnabled: false });
		e.preventDefault();
		if (this.state.password === "" || this.state.username === "") {
			alert("Podaj login i hasło!");
			return;
		}
		try {
			console.log("Ładowanie");
			const response = await axios.post<ILoginResponse>("/api/Session/Login",
				{
					Username: this.state.username,
					Password: this.state.password
				});
			console.log(response);
			if (response.status === 200) {
				const data = response.data;
				if (data.error) {
					this.setState<"errorText">({ errorText: data.message });
				} else {
					sessionStorage.setItem("sessionId", data.sessionId);
					return true;
				}
				this.setState<"buttonEnabled">({ buttonEnabled: true });
			}
		} catch (error) {
			if (error instanceof Error) {
				this.setState<"errorText">({
					errorText:
						"Wystąpił inny błąd. Otwórz konsolę przeglądarki (Ctrl + Shift + I -> 'Console') i wyślij zdjęcie administratorowi"
				});
				console.log("Error", error.message);
			}
			else console.log("Error", error);
		}
		this.setState<"buttonEnabled">({ buttonEnabled: true });
		return false;
	};

	render() {
		return (
			<Container style={{ width: "90%", maxWidth: "700px" }}>
				<div className="logo-container">
					<p>Kormoran Admin System</p>
				</div>
				<Row className="justify-content-sm-center">
					<form onSubmit={this.loginClick}>
						<FormFloating className="mb-3">
							<FormControl type="text" id="floatingInput"
								placeholder="Nazwa użytkownika" required={true}
								onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
									this.setState<"username">({ username: e.target.value });
								}} />
							<label htmlFor="floatingInput">Nazwa użytkownika</label>
						</FormFloating>
						<FormFloating>
							<FormControl type="password" id="floatingPassword"
								placeholder="Hasło" required={true}
								onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
									this.setState<"password">({ password: e.target.value });
								}}
							/>
							<label htmlFor="floatingPassword">Hasło</label>
						</FormFloating>
						<Row>
							<p className={this.state.errorText === "" ? "errorText-hidden" : "errorText mt-3"}>{this.state.errorText}</p>
						</Row>
						<Row>
							<Col className="d-grid">
								<Button variant="primary" className="p-3" type="submit" disabled={!this.state.buttonEnabled}>ZALOGUJ SIĘ</Button>
							</Col>
							<Col className="d-grid">
								<Button variant="primary" className="p-3" href="/Guest">GOŚĆ</Button>
							</Col>
						</Row>
					</form>
				</Row>
			</Container>
		);
	}
}

export default function (props: any) {
	const navigation = useNavigate();
	return <Login {...props} navigation={navigation} />
}
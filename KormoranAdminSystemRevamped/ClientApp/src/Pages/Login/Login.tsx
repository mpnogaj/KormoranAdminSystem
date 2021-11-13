import React from "react";
import axios from "axios";
import './Login.css';

interface IProps {

}

interface IState {
	username: string,
	password: string,
	buttonEnabled: boolean,
	errorText: string
}

class Login extends React.Component<IProps, IState>{
	constructor(props: any) {
		super(props);
		this.state = {
			username: "",
			password: "",
			buttonEnabled: true,
			errorText: ""
		};
	}

	loginClick = async (e: React.FormEvent) => {
		this.setState({buttonEnabled: false});
		e.preventDefault();
		if (this.state.password === "" || this.state.username === "") {
			alert("Podaj login i hasło!");
			return;
		}
		try {
			const response = await axios.post("http://localhost:5000/api/Administrate", {
				username: this.state.username,
				password: this.state.password
			});
			if (response.status === 200) {
				localStorage.setItem('username', this.state.username);
				localStorage.setItem('password', this.state.password);
				return;
			}
		}
		catch(error: any){
			if (error.response) {
				// Odpowiedź przyszła, status inny niż 200
				this.setState({errorText: "Nieprawdiłowa nazwa użytkownika lub hasło"});
			  } else if (error.request) {
				// Odpowiedź nie przyszła (404)
				this.setState({errorText: "Nie można połączyć się z serwerem. Powiadom administratora"});
			  } else {
				// Nie wiadomo co to
				this.setState({errorText: "Wystąpił inny błąd. Otwórz konsolę przeglądarki (ctrl + shift + i -> 'Console') i wyślij zdjęcie administratorowi"});
				console.log('Error', error.message);
			  }
		}
		this.setState({buttonEnabled: true});
	};

	render() {
		return (
			<div className="container mt-3">
				<div className="logo-container">
					<p>Kormoran Admin System</p>
				</div>
				<form onSubmit={this.loginClick}>
					<div className="form-floating mb-3">
						<input type="text" className="form-control" id="floatingInput"
							placeholder="Nazwa użytkownika" required={true}
							onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
								this.setState<"username">({ username: e.target.value });
							}}
						/>
						<label htmlFor="floatingInput">Nazwa użytkownika</label>
					</div>
					<div className="form-floating">
						<input type="password" className="form-control" id="floatingPassword"
							placeholder="Hasło" required={true}
							onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
								this.setState<"password">({ password: e.target.value });
							}}
						/>
						<label htmlFor="floatingPassword">Hasło</label>
					</div>
					<div className="row">
						<p className={this.state.errorText === "" ? "errorText-hidden" : "errorText mt-3"}>{this.state.errorText}</p>
					</div>
					<div className="row">
						<div className="col d-grid">
							<button className="btn btn-primary p-3" type="submit" disabled={!this.state.buttonEnabled}>ZALOGUJ SIĘ</button>
						</div>
						<div className="col d-grid">
							<a className="btn btn-primary p-3" href="/Guest">GOŚĆ</a>
						</div>
					</div>
				</form>
			</div>
		);
	}
}

export default Login;
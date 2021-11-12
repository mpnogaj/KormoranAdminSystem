import React from "react";
import axios from "axios";

interface IProps{

}

interface IState{
	username: string,
	password: string,
	loginBtnEnabled: boolean;
}

class Login extends React.Component<IProps, IState>{
    constructor(props: any){
		super(props);
		this.state = {
			username: "",
			password: "",
			loginBtnEnabled: false
		};
    }

	loginClick = async () => {
		if(this.state.password === "" || this.state.username === ""){
			alert("Podaj login i hasło!");
			return;
		}
		try{
			const response = await axios.post("http://localhost:5000/api/Administrate", {
				username: this.state.username,
				password: this.state.password
			});
			console.log(response);
		}
		catch(error){
			alert(error);
		}
	};

    render(){
        return( 
			<div>
				<input placeholder="zazwa użytkownika" onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                    this.setState<"username">({ username: e.target.value });
                }}/>
				<input placeholder="hasło" type="password" onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                    this.setState<"password">({ password: e.target.value });
                }}/>
				<button onClick={this.loginClick}>ZALOGUJ SIĘ</button>
			</div>
		);
    }
}

export default Login;
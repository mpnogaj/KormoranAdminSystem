import {render} from 'react-dom';
import {
	BrowserRouter,
	Routes,
	Route
} from 'react-router-dom'
import './index.css';
import App from './App';
import Guest from './Components/Guest/Guest';
import Login from './Components/Login/Login';

render(
	<BrowserRouter>
		<Routes>
			<Route path="/" element={<App/>}/>
			<Route path="Login" element={<Login/>}/>
			<Route path="Guest" element={<Guest/>}/>
		</Routes>
	</BrowserRouter>,
	document.getElementById('root')
);

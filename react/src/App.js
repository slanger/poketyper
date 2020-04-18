import React from 'react';
import SideBar from './components/sidebar/sidebar.js';
import logo from './logo.svg';
import Home from './pages/Home';
import Types from './pages/Types';
import Coverage from './pages/Coverage';
import Pokemon from './pages/Pokemon';
import './App.css';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from "react-router-dom";

function App() {
  return (
    <div className="App">
      <Router className="App">
        <SideBar />
        <Switch>
          <Route exact path="/" component={Home} />
          <Route path="/types" component={Types} />
          <Route path="/coverage" component={Coverage} />
          <Route path="/pokemon" component={Pokemon} />
        </Switch>
      </Router>
    </div>
  );
}

export default App;

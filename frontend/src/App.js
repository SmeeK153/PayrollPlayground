import React from 'react';
import './App.css';
import {
    BrowserRouter as Router,
    Switch,
    Route
} from "react-router-dom";

import Header from "./header/Header";
import Dashboard from "./dashboard/Dashboard";
import Company from "./company/Company";
import Employee from "./employee/Employee";

function App() {
    return (
        <div className="App">
            <header className="Payroll Playground">
                <Router>
                    <Header/>
                    <Switch>
                        <Route exact path="/">
                            <Dashboard/>
                        </Route>
                        <Route exact path="/company/:company">
                            <Company/>
                        </Route>
                        <Route exact path="/company/:company/employees/:employee">
                            <Employee />
                        </Route>
                    </Switch>
                </Router>
            </header>
        </div>
    );
}

export default App;

import ReactDOM from 'react-dom'
import React from 'react'

import Header from './header.jsx';

class App extends React.Component {
    render() {
        return (
            <React.Fragment>
                <Header />
                <div id="content_wrapper">
                    <div id="content" className="center">
                        <h2> OMG </h2>
                    </div>
                </div>
            </React.Fragment>
        );
    }
}

ReactDOM.render(
    <App />,
    document.getElementById('app')
);

/*
ReactDOM.render(
    <App />,
    document.getElementById('app')
);

import React from 'react';
import { render } from 'react-dom';

const App = () => (
    <React.Fragment>
        <h1>React in ASP.NET MVC!</h1>
        <div>Hello React World</div>
    </React.Fragment>
);

render(<App />, document.getElementById('app'));
*/

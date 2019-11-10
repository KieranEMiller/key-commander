import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect} from 'react-router-dom'

import ContentContainer from '../content.jsx';
import Loading from '../loading.jsx';
import SecureRouteComponent from '../secure_route_component.jsx';

class History extends SecureRouteComponent {
    constructor(props) {
        super(props);

        this.state = {
            isLoading: true,
            data: this.getUserHistory()
        }
    }

    getUserHistory() {
        var token = this.base.auth.getCurrentToken();
        return fetch('/api/User/History', {
                method: "Post",
                headers: { 'Content-Type': 'application/json', Accept: 'application/json' }
                , body: JSON.stringify(token)
            })
            .then(resp => resp.json())
            .then(data => {
                return Promise.resolve(data);
            });
    }

    render() {
        if (this.authenticationComplete()) {

            return (
                <ContentContainer authed={true}>
                    <h2>Session History</h2>

                    <div className="content_row">
                        <Loading showLoading={this.state.isLoading} />
                        
                        <table>
                            <thead>
                                <th>CreateDate</th>
                            </thead>
                            <tbody>
                                {this.state.data != null &&
                                    this.state.data.map((item, key) =>
                                        <tr key={item.SessionId}>{item.CreateDate}</tr>
                                )} 
                            </tbody>
                        </table>
                    </div>
                </ContentContainer>
            );
        }
        else {
            return (
                super.render()
            )
        }
    }
}

export default History;
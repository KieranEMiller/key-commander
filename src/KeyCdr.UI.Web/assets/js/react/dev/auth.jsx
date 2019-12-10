
import { LocalStorage, Urls, Runtime } from './constants.jsx';

class Auth {
    constructor(params) {
        this.loggedIn = false;
    }

    getCurrentToken() {
        var token = localStorage.getItem(LocalStorage.JWT_KEY_NAME);
        try {
            if (token)
                return JSON.parse(token);
        }
        catch (err) {
            console.log('auth: token parse error: ', err);
        }

        return null;
    }

    clearToken() {
        localStorage.removeItem(LocalStorage.JWT_KEY_NAME);
    }

    setToken(token) {
        var tokenstring = JSON.stringify(token);
        localStorage.setItem(LocalStorage.JWT_KEY_NAME, tokenstring);
    }

    /*synchronous auth*/
    /*
    isAuthenticated() {
        var token = this.getCurrentToken();
        if (!token || !token.jwt)
            return false;

        return (token && !this.isTokenExpired(token));
    }*/

    /*asynchronous auth*/
    isAuthenticated() {
        var that = this;
        return new Promise(function (resolve) {
            var token = that.getCurrentToken();
            if (!token) {
                resolve(false);
            }

            try {
                that.isValidToken(token)
                    .then(ret => {
                        if (Runtime.IS_DEBUG) console.log("auth: isauthenticated: server check was ", ret);
                        resolve(ret);
                    });
            }
            catch (err) {
                console.log("auth: isauthenticated: validity error ", err);
                resolve(false);
            }
        });
    }

    logout() {
        if (Runtime.IS_DEBUG) console.log("auth: logging out");
        this.clearToken();
    }

    login(user) {
        return fetch(Urls.API_AUTH_LOGIN, {
            method: "POST",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json'},
            body: JSON.stringify(user)
        })
        .then(resp => resp.json())
        .then(data => {
            if(Runtime.IS_DEBUG) console.log("login return: ", data);
            if (data.IsValid) {
                this.setToken(data);
            } 
            return Promise.resolve(data);
        })
    }

    isTokenExpired(token) {
        try {
            const decoded = decode(token);
            if (decoded.exp < Date.now() / 1000) { 
                return true;
            }
            else
                return false;
        }
        catch (err) {
            return false;
        }
    }

    makeRequestWithToken(url, data) {
        var token = this.getCurrentToken();
        if (!token || !token.JWTValue) return Promise.reject();

        return fetch(url, {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
                , body: JSON.stringify(data)
            })
            .then(resp => resp.json())
            .then(data => {
                return Promise.resolve(data);
            });
    }

    isValidToken() {
        var token = this.getCurrentToken();
        if (!token || !token.JWTValue) return Promise.resolve(false);

        return fetch(Urls.API_AUTH_VALIDATE, {
            method: "POST",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json' }
            , body: JSON.stringify(token)
        })
        .then(resp => resp.json())
        .then(data => {
            if (!data.IsValid) {
                this.clearToken();
            }
            return Promise.resolve(data.IsValid);
        });
    }

    isUsernameInUse(loginName) {

        if (loginName == "") return Promise.resolve(false);

        var req = { username: loginName };
        return fetch(Urls.API_AUTH_USERNAME_AVAILABLE, {
            method: "POST",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json' }
            , body: JSON.stringify(req)
        })
        .then(resp => resp.json())
        .then(data => {
            return Promise.resolve(data.IsInUse);
        });
    }

    register(loginName, password) {
        var user = { username: loginName, password: password};
        return fetch(Urls.API_AUTH_REGISTER, {
            method: "POST",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json' }
            , body: JSON.stringify(user)
        })
        .then(resp => resp.json())
            .then(data => {
            if(data.IsValid == true)
                this.setToken(data);
            return Promise.resolve(data);
        });
    }
};

export default Auth;

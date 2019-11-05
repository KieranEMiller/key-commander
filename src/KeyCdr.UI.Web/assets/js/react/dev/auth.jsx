
const AuthStatus = {
    isAuthenticated: false
}

class Auth {
    constructor(params) {
        this.DEBUG = true;
        this.URL_BASE = "http://localhost:8080/api/auth";
        this.LOCAL_STORAGE_KEY = "key-cdr-jwt";

        this.loggedIn = false;
    }

    getCurrentToken() {
        var token = localStorage.getItem(this.LOCAL_STORAGE_KEY);
        try {
            if (token)
                return JSON.parse(token);
        }
        catch (err) {
            console.log('token parse error: ', err);
        }

        return null;
    }

    clearToken() {
        localStorage.removeItem(this.LOCAL_STORAGE_KEY);
    }

    setToken(token) {
        var tokenstring = JSON.stringify(token);
        localStorage.setItem(this.LOCAL_STORAGE_KEY, tokenstring);
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
                        if (that.DEBUG) console.log("auth: isauthenticated: server check was ", ret);
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
        if (this.DEBUG) console.log("auth: logging out");
        this.clearToken();
    }

    login(user) {
        return fetch(this.URL_BASE + "/login", {
            method: "POST",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json'},
            body: JSON.stringify(user)
        })
        .then(resp => resp.json())
        .then(data => {
            if(this.DEBUG) console.log("login return: ", data);
            if (data.isvalid) {
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

    isValidToken() {
        var token = this.getCurrentToken();
        if (!token || !token.jwt) return Promise.resolve(false);

        return fetch(this.URL_BASE + "/validate", {
            method: "POST",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json'
                //'Authorization': `Bearer ${token}`
            }
            ,body: JSON.stringify(token)
        })
        .then(resp => resp.json())
        .then(data => {
            if (!data.isvalid) {
                this.clearToken();
            }
            return Promise.resolve(data.isvalid);
        })
    }
};

export default Auth;

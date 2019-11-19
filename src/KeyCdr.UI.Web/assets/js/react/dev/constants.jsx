
export const Paths = {
    CSS_MASTER: "../../../../assets/css/master.scss",
    AUTH: './'
}

export const Urls = {
    API_AUTH_VALIDATE: "http://localhost:8080/api/auth/validate",
    API_AUTH_LOGIN: "http://localhost:8080/api/auth/login",
    API_HISTORY: "/api/User/History",
    API_HISTORY_DETAILS: "/api/User/HistoryDetails",

    External: {
        JS: {
            JQUERY: "https://code.jquery.com/jquery-3.4.1.min.js",
            DATATABLES_CORE: "https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js",
            DATATABLES_ROWGROUP: "https://cdn.datatables.net/rowgroup/1.1.1/js/dataTables.rowGroup.min.js"
        },

        CSS: {
            DATATABLES_CORE: "https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css",
            DATATABLES_ALT: "https://cdn.datatables.net/rowgroup/1.1.1/css/rowGroup.dataTables.min.css"
        }
    }
}

export const Routes = {
    DEFAULT: "/",
    INDEX: "/",

    LOGIN: "/Login",
    LOGOUT: "/Logout",
    NEW_SEQUENCE: "/NewSequence",

    MY_ACCT: "/secure/MyAccount",
    HISTORY: "/secure/History",
    HISTORY_DETAILS: "/secure/HistoryDetails",
    HISTORY_DETAILS_WITHID: "/secure/HistoryDetails/:sequenceId"
}

export const LocalStorage = {
    JWT_KEY_NAME: "key-cdr-jwt"
}

export const Runtime = {
    IS_DEBUG: true
}

export const ErrorMsgs = {
    AUTH_FAILURE: "unable to login with the username and password provided"
}
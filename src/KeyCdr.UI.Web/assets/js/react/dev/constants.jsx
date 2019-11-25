
export const Urls = {
    API_AUTH_VALIDATE: "http://localhost:8080/api/auth/validate",
    API_AUTH_LOGIN: "http://localhost:8080/api/auth/login",
    API_HISTORY: "/api/User/History",
    API_HISTORY_DETAILS: "/api/User/HistoryDetails",
    API_HISTORY_VISUAL: "/api/User/HistoryAnalytics",

    API_NEW_SESSION: "/api/Session/GetNewSession",
    API_NEW_SEQUENCE: "/api/Sequence/GetNewSequence",
    API_END_SEQUENCE: "/api/Sequence/EndSequence",
    API_RUN_ANALYSIS_FOR_SEQ: "/api/ErrorAnalysis/RunForSequence",

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
    NEW_SEQUENCE: "/secure/NewSequence",

    MY_ACCT: "/secure/MyAccount",
    HISTORY: "/secure/History",
    HISTORY_DETAILS: "/secure/HistoryDetails",
    HISTORY_DETAILS_WITHID: "/secure/HistoryDetails/:sequenceId",
    HISTORY_VISUAL: "/secure/History/Visual"
}

export const LocalStorage = {
    JWT_KEY_NAME: "key-cdr-jwt",
    SESSION_KEY_NAME: "key-cdr-session"
}

export const Runtime = {
    IS_DEBUG: true,
    DEFAULT_NEW_SEQUENCE_INPUT_FIELD_HEIGHT: 75
}

export const UserMsgs = {
    AUTH_FAILURE: "unable to login with the username and password provided",
    NEW_SEQUENCE_INSTRUCTIONS: "PRESS SHIFT+ENTER OR PRESS START TO BEGIN"
}

export const HighlightType = {
    0: "IncorrectChar",
    1: "ExtraChars", 
    2: "ShortChars", 
    3: "Normal",
    4: "Unevaluated"
}

export const HttpErrorHandler = function (response) {
    if (!response.ok) {
        throw Error(response.statusText);
    }
    return response;
}
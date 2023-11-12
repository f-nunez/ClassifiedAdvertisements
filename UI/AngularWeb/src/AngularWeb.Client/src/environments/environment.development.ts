export const environment = {
    production: false,
    apiUrl: 'https://localhost:7220/',
    oidcSetting: {
        authority: 'https://localhost:7210/',
        automaticSilentRenew: false,
        clientId: '6c4c5801-1089-4c3c-83c7-ddc0eb3707b3',
        postLogoutRedirectUri: 'http://localhost:4200/',
        redirectUri: 'http://localhost:4200/signin-callback',
        responseType: 'code',
        scope: 'openid profile email roles ads_command_api ads_query_api angular_web_api',
        silentRedirectUri: 'http://localhost:4200/silent-callback.html'
    },
    useFakeAuth: false,
    useCookieBasedAuthentication: true
};

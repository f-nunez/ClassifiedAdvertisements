export const environment = {
    production: false,
    apiUrl: 'https://localhost:44417/',
    oidcSetting: {
        authority: 'https://localhost:44417/',
        automaticSilentRenew: false,
        clientId: '6c4c5801-1089-4c3c-83c7-ddc0eb3707b3',
        postLogoutRedirectUri: 'https://localhost:44417/',
        redirectUri: 'https://localhost:44417/signin-callback',
        responseType: 'code',
        scope: 'openid profile email roles ads_command_api ads_query_api angular_web_api',
        silentRedirectUri: 'https://localhost:44417/silent-callback.html'
    },
    useFakeAuth: false,
    useCookieBasedAuthentication: true
};

export const environment = {
    production: true,
    apiUrl: '',
    oidcSetting: {
        authority: 'https://localhost:7210/',
        automaticSilentRenew: false,
        clientId: '6c4c5801-1089-4c3c-83c7-ddc0eb3707b3',
        postLogoutRedirectUri: 'https://localhost:7220/',
        redirectUri: 'https://localhost:7220/signin-callback',
        responseType: 'code',
        scope: 'openid profile email roles ads_command_api ads_query_api angular_web_api',
        silentRedirectUri: 'https://localhost:7220/silent-callback.html'
    },
    useFakeAuth: false,
    useCookieBasedAuthentication: true
};

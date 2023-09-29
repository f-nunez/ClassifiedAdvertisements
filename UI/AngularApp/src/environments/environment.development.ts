export const environment = {
    oidcSetting: {
        authority: 'https://localhost:7210/',
        automaticSilentRenew: true,
        clientId: '6c4c5801-1089-4c3c-83c7-ddc0eb3707b3',
        postLogoutRedirectUri: 'http://localhost:4200/',
        redirectUri: 'http://localhost:4200/signin-callback',
        responseType: 'code',
        scope: 'openid profile email ads_api roles',
        silentRedirectUri: 'http://localhost:4200/silent-callback.html'
    },
    useFakeAuth: true,
};

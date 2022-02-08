import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'SiteBuilding',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44346',
    redirectUri: baseUrl,
    clientId: 'SiteBuilding_App',
    responseType: 'code',
    scope: 'offline_access SiteBuilding',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44346',
      rootNamespace: 'Dignite.SiteBuilding',
    },
    SiteBuilding: {
      url: 'https://localhost:44366',
      rootNamespace: 'Dignite.SiteBuilding',
    },
  },
} as Environment;

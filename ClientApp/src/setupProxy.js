const { createProxyMiddleware } = require('http-proxy-middleware');
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:42495';

const context = [
    "/weatherforecast",
    "/swagger",
    "/Auth",
    "/Auth/Login",
    "/Component",
    "/Component/AddNewComponent",
    "/Component/UpdateComponentPrice",
    "/Component/GetAll",
    "/Component/GetAvailableComponent",
    "/Component/GetMissingComponents",
    "/User",
    "/Storage",
    "/Project",
    "/Project/AddWorkTimeAndFee",
    "/Project/AddComponentToProject",
    "/Project/GetProjects",
    "/Project/GetProjectComponents",
    "/Project/AddNewProject",
    "/Project/GetProjectsWithStatus",
    "/Project/GetProjectsComponentsInformation",
];

module.exports = function(app) {
  const appProxy = createProxyMiddleware(context, {
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  });

  app.use(appProxy);
};

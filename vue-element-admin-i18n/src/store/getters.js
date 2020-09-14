const getters = {
  sidebar: state => state.app.sidebar,
  language: state => state.app.language,
  size: state => state.app.size,
  device: state => state.app.device,
  visitedViews: state => state.tagsView.visitedViews,
  cachedViews: state => state.tagsView.cachedViews,
  token: state => state.user.token,
  avatar: state => state.user.avatar,
  name: state => state.user.name,
  userName: state => state.user.userName,
  introduction: state => state.user.introduction,
  roles: state => state.user.roles,
  email: state => state.user.email,
  phoneNumber: state => state.user.phoneNumber,
  permission_routes: state => state.permission.routes,
  errorLogs: state => state.errorLog.logs,
  abpConfig: state => state.app.abpConfig,
  tenant: state => state.app.tenant
}
export default getters

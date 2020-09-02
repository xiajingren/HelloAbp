import Layout from '@/layout'

const settingRouter = {
  path: '/settings',
  component: Layout,
  // redirect: 'noRedirect',
  // alwaysShow: true,
  // name: 'Settings',
  // meta: {
  //   title: 'AbpSettingManagement["Settings"]',
  //   icon: 'el-icon-setting',
  //   policy: ''
  // },
  children: [
    {
      path: 'setting',
      component: () => import('@/views/settings/setting'),
      name: 'Setting',
      meta: {
        title: 'AbpSettingManagement["Settings"]',
        policy: 'SettingUi.Tenant',
        icon: 'el-icon-setting'
      }
    }
  ]
}

export default settingRouter

import Layout from '@/layout'

const settingRouter = {
  path: '/setting',
  component: Layout,
  // redirect: 'noRedirect',
  // alwaysShow: true,
  // name: 'Setting',
  // meta: {
  //   title: 'AbpSettingManagement["Settings"]',
  //   icon: 'el-icon-setting',
  //   policy: ''
  // },
  children: [
    {
      path: 'businessRule',
      component: () => import('@/views/settings/setting'),
      name: 'BusinessRule',
      meta: {
        title: 'AbpSettingManagement["Settings"]',
        policy: 'SettingUi.Tenant',
        icon: 'el-icon-setting',
        affix: true
      }
    }
  ]
}

export default settingRouter

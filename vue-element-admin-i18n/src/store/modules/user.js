import { login, logout, getInfo } from '@/api/user'
import { getToken, setToken, removeToken } from '@/utils/auth'
import { resetRouter } from '@/router'

const clientSetting = {
  grant_type: 'password',
  scope: 'HelloAbp',
  username: '',
  password: '',
  client_id: 'HelloAbp_App',
  client_secret: '1q2w3e*'
}

const state = {
  token: getToken(),
  name: '',
  avatar: '',
  email: '',
  introduction: '',
  roles: []
}

const mutations = {
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_INTRODUCTION: (state, introduction) => {
    state.introduction = introduction
  },
  SET_NAME: (state, name) => {
    state.name = name
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
  },
  SET_ROLES: (state, roles) => {
    state.roles = roles
  },
  SET_EMAIL: (state, email) => {
    state.email = email
  },
  CLEAN: state => {
    state.token = ''
    state.name = ''
    state.avatar = ''
    state.introduction = ''
    state.roles = []
  }
}

const actions = {
  // user login
  login({ commit }, userInfo) {
    const { username, password } = userInfo
    return new Promise((resolve, reject) => {
      clientSetting.username = username.trim()
      clientSetting.password = password
      login(clientSetting)
        .then(response => {
          commit('SET_TOKEN', response.access_token)
          setToken(response.access_token).then(() => {
            resolve()
          })
        })
        .catch(error => {
          reject(error)
        })
    })
  },

  // get user info
  getInfo({ commit }) {
    return new Promise((resolve, reject) => {
      getInfo()
        .then(response => {
          if (!response) {
            reject('Verification failed, please Login again.')
          }

          const { name, email, extraProperties } = response

          commit('SET_NAME', name)
          commit('SET_AVATAR', extraProperties.Avatar)
          commit('SET_EMAIL', email)
          commit('SET_INTRODUCTION', extraProperties.Introduction)
          resolve(response)
        })
        .catch(error => {
          reject(error)
        })
    })
  },

  setRoles({ commit }, roles) {
    commit('SET_ROLES', roles)
  },

  // user logout
  logout({ commit, dispatch }) {
    return new Promise((resolve, reject) => {
      logout()
        .then(() => {
          commit('CLEAN')
          removeToken().then(() => {
            resetRouter()
            // reset visited views and cached views
            // to fixed https://github.com/PanJiaChen/vue-element-admin/issues/2485
            dispatch('tagsView/delAllViews', null, { root: true })

            resolve()
          })
        })
        .catch(error => {
          reject(error)
        })
    })
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      commit('CLEAN')
      removeToken().then(() => {
        resolve()
      })
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}

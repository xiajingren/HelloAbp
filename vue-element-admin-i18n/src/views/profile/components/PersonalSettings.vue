<template>
  <el-form ref="aForm" :model="userInfo" :rules="aRules">
    <el-form-item :label="$t('AbpAccount[\'DisplayName:Name\']')" prop="name">
      <el-input v-model.trim="userInfo.name" />
    </el-form-item>
    <el-form-item :label="$t('AbpAccount[\'DisplayName:Email\']')" prop="email">
      <el-input v-model.trim="userInfo.email" />
    </el-form-item>
    <el-form-item :label="$t('AbpAccount[\'DisplayName:UserName\']')" prop="userName">
      <el-input v-model.trim="userInfo.userName" />
    </el-form-item>
    <el-form-item :label="$t('AbpAccount[\'DisplayName:PhoneNumber\']')" prop="phoneNumber">
      <el-input v-model.trim="userInfo.phoneNumber" />
    </el-form-item>
    <el-form-item label="个人介绍">
      <el-input v-model.trim="userInfo.extraProperties.Introduction" type="textarea" :rows="2" placeholder="请输入个人介绍" />
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="submit">{{ $t("AbpAccount['Submit']") }}</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
export default {
  props: {
    user: {
      type: Object,
      default: () => {
        return {
          name: '',
          userName: '',
          email: '',
          avatar: '',
          role: '',
          phoneNumber: '',
          introduction: ''
        }
      }
    }
  },
  data() {
    return {
      aRules: {
        name: [{
          required: true,
          message: this.$i18n.t("AbpAccount['ThisFieldIsRequired.']"),
          trigger: ['blur', 'change']
        }
        ],
        email: [{
          required: true,
          message: this.$i18n.t("AbpAccount['ThisFieldIsRequired.']"),
          trigger: ['blur', 'change']
        }
        ],
        UserName: [{
          required: true,
          message: this.$i18n.t("AbpAccount['ThisFieldIsRequired.']"),
          trigger: ['blur', 'change']
        }
        ],
        phoneNumber: [{
          required: true,
          message: this.$i18n.t("AbpAccount['ThisFieldIsRequired.']"),
          trigger: ['blur', 'change']
        }
        ]
      },
      loading: false,
      userInfo: {
        userName: this.user.userName,
        email: this.user.email,
        name: this.user.name,
        phoneNumber: this.user.phoneNumber,
        extraProperties: {
          Avatar: this.user.avatar,
          Introduction: this.user.introduction
        }
      }
    }
  },
  mounted() {
    // console.log(this.user)
  },
  methods: {
    submit() {
      this.$refs.aForm.validate(valid => {
        if (valid) {
          this.loading = true
          this.$store.dispatch('user/setUserInfo', this.userInfo)
            .then((res) => {
              this.loading = false
              this.$notify({
                title: this.$i18n.t("HelloAbp['Success']"),
                message: this.$i18n.t("HelloAbp['SuccessMessage']"),
                type: 'success',
                duration: 2000
              })
            }).catch(() => {})
        } else {
          return false
        }
      })
    }
  }
}
</script>

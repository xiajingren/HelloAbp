<template>
  <el-form ref="aForm" :model="aForm" :rules="aRules">
    <el-form-item :label="$t('AbpAccount[\'DisplayName:CurrentPassword\']')" prop="password">
      <el-input v-model.trim="aForm.password" type="password" />
    </el-form-item>
    <el-form-item :label="$t('AbpAccount[\'DisplayName:NewPassword\']')" prop="newPassword">
      <el-input v-model.trim="aForm.newPassword" type="password" />
    </el-form-item>
    <el-form-item :label="$t('AbpAccount[\'DisplayName:NewPasswordConfirm\']')" prop="againPassword">
      <el-input v-model.trim="aForm.againPassword" type="password" />
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="submit">{{ $t("AbpAccount['Submit']") }}</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
import { changePassword } from '@/api/user'
export default {
  data() {
    var avalidatePass = (rule, value, callback) => {
      if (value === '') {
        callback(new Error(
          this.$i18n.t("AbpAccount['ThisFieldIsNotValid.']")
        ))
      } else if (value !== this.aForm.newPassword) {
        callback(new Error(
          this.$i18n.t(
            "AbpIdentity['{0} and {1} do not match.']",
            [this.$i18n.t("AbpAccount['DisplayName:NewPassword']"),
              this.$i18n.t("AbpAccount['DisplayName:NewPasswordConfirm']")]
          )
        ))
      } else {
        callback()
      }
    }
    return {
      aForm: {
        password: '',
        newPassword: '',
        againPassword: ''
      },
      aRules: {
        password: [{
          required: true,
          message: this.$i18n.t("AbpAccount['ThisFieldIsRequired.']"),
          trigger: ['blur', 'change']
        }
        ],
        newPassword: [{
          required: true,
          message: this.$i18n.t("AbpAccount['ThisFieldIsRequired.']"),
          trigger: ['blur', 'change']
        }
        ],
        againPassword: [{
          required: true,
          message: this.$i18n.t("AbpAccount['ThisFieldIsRequired.']"),
          validator: avalidatePass,
          trigger: ['blur', 'change']
        }
        ]
      },
      loading: false
    }
  },
  methods: {
    submit() {
      this.$refs.aForm.validate(valid => {
        if (valid) {
          this.loading = true
          const dataJson = {
            currentPassword: this.aForm.password,
            newPassword: this.aForm.againPassword
          }
          changePassword(dataJson).then((res) => {
            this.loading = false
            this.$notify({
              title: this.$i18n.t("HelloAbp['Success']"),
              message: this.$i18n.t("HelloAbp['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
            .catch(() => {
              this.loading = false
            })
        } else {
          return false
        }
      })
    }
  }
}
</script>

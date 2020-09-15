<template>
  <el-card style="margin-bottom:20px;">
    <div slot="header" class="clearfix">
      <span>{{ $t("userCard.aboutMe") }}</span>
    </div>

    <div class="user-profile">
      <div class="box-center">
        <pan-thumb
          :image="getFilePathByName(user.avatar)"
          :height="'100px'"
          :width="'100px'"
          :hoverable="false"
        >
          <div>{{ $t("userCard.greetings") }}</div>
          {{ user.name }}
        </pan-thumb>
      </div>
      <div />
      <div class="box-center">
        <div class="user-name text-center">{{ user.name }}</div>
        <div class="user-role text-center text-muted">
          {{ user.role | uppercaseFirst }}
        </div>
      </div>
      <div class="box-center">
        <el-upload
          action
          name="file"
          :before-upload="beforeUpload"
          :http-request="uploadAvatar"
          :show-file-list="false"
        >
          <el-button type="primary" icon="el-icon-upload">{{
            $t("userCard.changeAvatar")
          }}</el-button>
        </el-upload>
      </div>
    </div>
    <div class="user-bio">
      <div class="user-education user-bio-section">
        <div class="user-bio-section-header">
          <svg-icon icon-class="education" />
          <span>{{ $t("userCard.personalIntroduction") }}</span>
        </div>
        <div class="user-bio-section-body">
          <div class="text-muted">
            {{
              user.introduction
                ? user.introduction
                : $t("userCard.personalIntroductionContent")
            }}
          </div>
        </div>
      </div>
    </div>
  </el-card>
</template>

<script>
import PanThumb from '@/components/PanThumb'
import { createFile } from '@/api/file-management/file'
import { getFilePathByName } from '@/utils/abp'
export default {
  components: { PanThumb },
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
      loading: false
    }
  },
  mounted() {},
  methods: {
    getFilePathByName,
    beforeUpload(file) {
      // TODO: Image format verification

    },
    uploadAvatar(data) {
      const fd = new FormData()
      fd.append('file', data.file)
      createFile(fd).then(resData => {
        this.user.avatar = resData
        const userInfo = {
          userName: this.user.userName,
          email: this.user.email,
          name: this.user.name,
          phoneNumber: this.user.phoneNumber,
          extraProperties: {
            Avatar: resData,
            Introduction: this.user.introduction
          }
        }
        this.loading = true
        this.$store.dispatch('user/setUserInfo', userInfo).then(res => {
          this.loading = false
          this.$notify({
            title: this.$i18n.t("HelloAbp['Success']"),
            message: this.$i18n.t("HelloAbp['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    }
  }
}
</script>

<style lang="scss" scoped>
.box-center {
  margin: 0 auto;
  display: table;
}

.text-muted {
  color: #777;
}

.user-profile {
  .user-name {
    font-weight: bold;
  }

  .box-center {
    padding-top: 10px;
  }

  .user-role {
    padding-top: 10px;
    font-weight: 400;
    font-size: 14px;
  }

  .box-social {
    padding-top: 30px;

    .el-table {
      border-top: 1px solid #dfe6ec;
    }
  }

  .user-follow {
    padding-top: 20px;
  }
}

.user-bio {
  margin-top: 20px;
  color: #606266;

  span {
    padding-left: 4px;
  }

  .user-bio-section {
    font-size: 14px;
    padding: 15px 0;

    .user-bio-section-header {
      border-bottom: 1px solid #dfe6ec;
      padding-bottom: 10px;
      margin-bottom: 10px;
      font-weight: bold;
    }
  }
}
</style>

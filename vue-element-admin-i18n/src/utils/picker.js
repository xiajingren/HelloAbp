import moment from 'moment'
import 'moment/locale/zh-cn'
moment.locale('zh-cn')
export const pickerRangeWithHotKey = {
  shortcuts: [{
    text: '当天',
    onClick(picker) {
      const end = moment().endOf('day').format('YYYY-MM-DD HH:mm:ss')
      const start = moment().startOf('day').format('YYYY-MM-DD HH:mm:ss')
      picker.$emit('pick', [start, end])
    }
  },
  {
    text: '最近一周',
    onClick(picker) {
      const end = moment().endOf('day').format('YYYY-MM-DD HH:mm:ss')
      const start = moment().endOf('day').subtract(7, 'days')
      picker.$emit('pick', [start, end])
    }
  },
  {
    text: '最近一个月',
    onClick(picker) {
      const end = moment().endOf('day').format('YYYY-MM-DD HH:mm:ss')
      const start = moment().endOf('day').subtract(1, 'months').format('YYYY-MM-DD HH:mm:ss')
      picker.$emit('pick', [start, end])
    }
  },
  {
    text: '最近三个月',
    onClick(picker) {
      const end = moment().endOf('day').format('YYYY-MM-DD HH:mm:ss')
      const start = moment().endOf('day').subtract(3, 'months').format('YYYY-MM-DD HH:mm:ss')
      picker.$emit('pick', [start, end])
    }
  }]
}

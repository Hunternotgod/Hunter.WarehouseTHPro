# Hunter.WarehouseTHPro
基于C#的仓储温湿度监控系统

### ✅ 各模块引用详情
1. Hunter.WarehouseTHPro (表现层)
引用了：
- ✅ Hunter.WarehouseTHBLL
- ✅ Hunter.WarehouseTHHelper
- ✅ Hunter.WarehouseTHModels
  
2. Hunter.WarehouseTHBLL (业务逻辑层)
引用了：
- ✅ Hunter.WarehouseTHDAL
- ✅ Hunter.WarehouseTHHelper
- ✅ Hunter.WarehouseTHModels

3. Hunter.WarehouseTHDAL (数据访问层)
引用了：
- ✅ Hunter.WarehouseTHModels
- ⚠️ 缺少 Hunter.WarehouseTHHelper 引用（后续按需添加）

4. Hunter.WarehouseTHModels (数据模型层)
- ✅ Hunter.WarehouseTHHelper

5. Hunter.WarehouseTHHelper (辅助工具层)
- 无引用（作为通用工具）

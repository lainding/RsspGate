# RsspGate
---------------------------------------------
**功能：**

进行协议转换与转发

![AltText](./documents/flowchart.png)

## **配置**

配置`config.json`包括3部分：分别定义设备，端口和路由方式。

1. 端口`gates`

  RsspGate开启的端口，可与设备进行通信。
  * `name`：开启端口名称（**必选项**）
  * `ip`：端口绑定IP地址（可省略，省略后对本机所有地址监听）
  * `port`：监听端口
  * `type`：端口类型（UDP，TCP(*TODO*)）

2. 设备`devices`

  一个远端端口被定义为一个设备。
  * `name`： 设备名称（**必选项**）
  * `ip`： 设备IP地址（可省略，缺省值为127.0.0.1）
  * `port`：设备端口号（**必选项**）

3. 路由规则`routes`

  定义消息转化规则。此段定义的含义是通过`through`端口接收`from`来的消息，经过`process`定义的处理方法处理后，通过`by`端口发送给`to`。
  * `from`：来源设备，设备名称，使用之前在设备中定义过的设备
  * `to`：目标设备，设备名称，使用之前在设备中定义过的设备
  * `through`：通过此端口接收来源设备发送的消息，使用之前定义过的端口
  * `by`：通过此端口发送消息到目标设备，使用之前定义过的端口
  * `process`：定义处理方法，可定义多个，按照顺序依次处理后

## 处理方法

1. 处理方法参数
  * `name`：处理方法（包含`insert`，`remove`，`direct`，`reverse`，`change`，`block`等）
  * `parameters`：处理方法参数

1. 插入`insert`
  在通信的消息中插入信息。
  * `position`： 插入信息的位置（可选值：数值——表示在消息中的位置，`start/begin`——表示在消息的开始，`end`——表示在消息尾部
  * `addon`：定义插入内容
    * `function`： 插入内容函数（包括`static`，`timestamp`,`sequence`等）
    * `data`：插入内容函数参数
        * `type`：插入内容数据类型（`string`，`byte`，`int`，`short`，`long`，`uint`，`ushort`，`ulong`，`float`，`double`，`array`）
        * `encoding`：字符串编码方式（`ASCII`，`UTF8`，`UNICODE`，`UTF32`）
        * `length`：长度
        * `endian`：大小端方式（`big`，`little`）
      
    > **static**
    >
    > 插入静态值。
    > * `value`：插入值内容

    > **timestamp**
    > 
    > 插入时间戳，时间戳目前是4个字节。

    > **sequence**
    >  
    > 插入序号
    > * `loop`：序号是否循环
    > * `type` ：



























































**TODO**
* []增加设置条件功能
* [tcp]增加TCP端口实现
* []增加Value产生方法

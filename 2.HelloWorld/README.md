# HelloWorld 文档翻译

## 介绍

RabbitMQ 是一个 ** message  broker ** （消息代理人）：它接受消息然后发送出去。你可以将它理解为一个邮局：当你向邮箱投递邮件时，你可以确定会有某个邮递员来将你的邮件送达你的收件人。在这个例子中，RabbitMQ就相当于邮箱、邮局和有邮递员。

RabbitMQ和邮局不同的是，它处理的不是纸张，而是数据消息转化而来的二进制对象。

RabbitMQ常使用如下的术语：

* ** Producing ** 表示发送。一个发送消息的程序我们就称它是一个 ** producer  **

* 一个** Queue ** 可以理解为RabbitMQ中某个邮箱的编号。并且，在我们的应用同RabbitMQ交互消息时，这些消息必须且仅能存储在一个queue中。一个queue的存储量与服务端的内存和硬盘大小有关，它本质上是一个大的消息缓存。多个 ** producers ** 可以向同一个queue发送消息，多个 ** consumers  ** 也可以消费同一个queue。

* ** Consuming ** 和 ** Producing ** 意义差不多。一个 ** consumer  ** 就表示一个正在等待接受消息的程序。

值得注意的是，producer, consumer, 和 broker不是必须要部署在同一台服务器上的，事实情况是，绝大部分应用都不是部署在一起的。一个应用通常即是producer，同时又是consumer。


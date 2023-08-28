<div id="MainDiv" style="width: 100%;height: 100%;border: 0px solid red;">
        <div class="navbar">
            <div class="title-container">
                <div class="title">Yam Music Player Helper-Yam 音乐播放器-v1.0</div>
            </div>
        </div>
        <div id="ContentDiv" style="width: 100%;height: 85%;border: 0px solid red;overflow-x: hidden;overflow-y: auto;" >
            <img class="bigLogo" src="https://github.com/yam126/YamMusicPlayer/blob/master/WpfNetMusic/Image/SplashScreenBG.png?raw=true" />
            <div class="title" style="width: 35%;height: 13vh;font-family: 'Microsoft YaHei UI';font-size: 5vw;font-weight: 700;color: #99cee5d1;text-align: center;margin-top: 5vh;margin-left: 32.5%;text-shadow: 3px 7px 1px #3f6a8f8a, -1px 3px 16px #e0d6d6;border: 0px solid red;float: left;" >
                帮助文件
            </div>
            本程序需要.net6.0 sdk<br/>
            IDE为Visual Studio 2022<br/>
            文件夹说明<br/>
            DataAccess 为数据访问库<br/>
            SetupFiles 为安装程序压缩包<br/>
            Snowflake.Net 为雪花ID组件<br/>
            TestProject 为测试项目<br/>
            WpfNetMusic为项目主体<br/>
            WpfNetMusic.sln 为解决方案文件<br/>
            WpfNetMusicDB.db 为必须的Sqlite文件安装好程序后放在程序安装目录的根目录即可<br/>
            <div class="Label">
                启动页
            </div>
            <div class="Content">
                当程序启动时会弹出是否全盘搜索的对话框,选择全盘搜索之后会搜索电脑里的所有MP3文件，
                <span class="markednessTag">注意:如果选择全盘搜索,此过程较为漫长</span>建议选择文
                件夹进行搜索<br />
                <div class="imgContainer">
                    <img src="https://github.com/yam126/YamMusicPlayer/blob/master/WpfNetMusic/Image/HelpImage/img01.png?raw=true" />
                </div>
                选择否会弹出文件夹选择窗口，选择存有mp3文件的文件夹然后点击确定，程序就会开始解析mp3文件<br />
                <div class="imgContainer">
                    <img style="margin-left:2%;" src="https://github.com/yam126/YamMusicPlayer/blob/master/WpfNetMusic/Image/HelpImage/img02.png?raw=true" />
                </div>
                之后启动页就开始分析文件夹里找到的mp3文件<br />
                <div class="imgContainer">
                    <img style="margin-left:2%;" src="https://github.com/yam126/YamMusicPlayer/blob/master/WpfNetMusic/Image/HelpImage/img04.png?raw=true" />
                </div>
                如果找到的文件中有错误文件就会弹出错误消息对话框<br />
                <div class="imgContainer">
                    <img style="margin-left:2%;width:64vw;height:41vh;" src="https://github.com/yam126/YamMusicPlayer/blob/master/WpfNetMusic/Image/HelpImage/img03.png?raw=true" />
                </div>
            </div>
            <div class="Label">
                主界面
            </div>
            <div class="Content">
                主界面的显示如下图<br />
                <div class="imgContainer">
                    <img style="margin-left:2%;" src="https://github.com/yam126/YamMusicPlayer/blob/master/WpfNetMusic/Image/HelpImage/img05.png?raw=true" />
                </div>
                1、标题栏区域,从左到右依次为<br />
                &nbsp;&nbsp;1 程序的图标和标题<br />
                &nbsp;&nbsp;1.2 搜索当前歌单列表歌曲(输入关键字按下回车开始搜索)<br />
                &nbsp;&nbsp;1.3 主菜单按钮(点击弹出主菜单)<br />
                &nbsp;&nbsp;1.4 最小化<br />
                &nbsp;&nbsp;1.5 最大化<br />
                &nbsp;&nbsp;1.6 关闭窗口<br />
                2、提示和批量添加区域，从左到右依次为<br />
                &nbsp;&nbsp;2.1 显示批量添加的文件夹路径的文本框<br />
                &nbsp;&nbsp;2.2 浏览批要量添加的文件夹(选择后自动添加选中文件夹中的音乐到当前歌单)<br />
                &nbsp;&nbsp;2.3 当前正在播放的歌单名称<br />
                3、当前歌单的音乐列表,鼠标双击其中一项则开始播放选中的音乐,鼠标右键单击弹出的快捷菜单可以对音乐进行操作<br />
                4、歌单列表,鼠标单击右键可以对歌单进行操作<br />
                <span class="markednessTag">注意:默认歌单不可删除,一定要删除必须指定其他歌单为默认歌单即可删除</span><br />
                5、播放控制区域,从左到右依次为<br />
                &nbsp;&nbsp;5.1当前播放歌曲显示<br />
                &nbsp;&nbsp;5.2播放顺序控制按钮 <br />
                &nbsp;&nbsp;5.3上一首歌曲按钮<br />
                &nbsp;&nbsp;5.4播放和暂停按钮(双击播放或暂停播放)<br />
                &nbsp;&nbsp;5.5下一首歌曲按钮<br />
                &nbsp;&nbsp;5.6歌词显示按钮,歌词格式为ylrc<br />
                &nbsp;&nbsp;&nbsp;歌词文件的内容如下<br />
                &nbsp;&nbsp;&nbsp;&nbsp;[00:19.64]あの日見渡した渚を<br />
                &nbsp;&nbsp;&nbsp;&nbsp;[00:24.50]今も思い出すんだ<br />
                &nbsp;&nbsp;&nbsp;&nbsp;[00:19.64]代表歌词出现的时间,格式为[分钟:秒钟.毫秒]<br />
                &nbsp;&nbsp;5.7为音量条件按钮,可以条件系统的音量
                6、歌曲进度条控件，拖动控制点可以调整播放进度<br />
            </div>
            <div class="Label">
                拖拽添加歌曲
            </div>
            <div class="Content">
                可以通过将外部文件鼠标拖拽到歌曲列表添加歌曲<br />
                <div class="imgContainer">
                    <img style="margin-left:2%;" src="https://github.com/yam126/YamMusicPlayer/blob/master/WpfNetMusic/Image/HelpImage/img06.gif?raw=true" />
                </div>
            </div>
            <div class="Label">
                拖拽移动歌曲到歌单
            </div>
            <div class="Content">
                可以在歌单列表单击鼠标拖拽歌曲文件到歌单列表进行移动<br />
                <div class="imgContainer">
                    <img style="margin-left:2%;" src="https://github.com/yam126/YamMusicPlayer/blob/master/WpfNetMusic/Image/HelpImage/img07.gif?raw=true" />
                </div>
            </div>
</div>



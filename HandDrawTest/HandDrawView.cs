using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Graphics;

namespace HandDrawTest
{
    /// <summary>
    /// 实现一个画图板,当手指在触摸屏上移动时,即可在屏幕上绘制任意的图形
    /// </summary>
    public class HandDrawView : View
    {
        /// <summary>
        /// 记录当前的x坐标
        /// </summary>
        float preX;

        /// <summary>
        /// 记录当前的y坐标
        /// </summary>
        float preY;

        /// <summary>
        /// 自定义画图板的绘制路径对象
        /// </summary>
        private Path path;

        /// <summary>
        /// 自定义画图板的画笔对象
        /// </summary>
        public Paint paint = null;


        const int VIEW_WIDTH = 320;
        const int VIEW_HEIGHT = 480;

        /// <summary>
        /// 定义一个内存中的图片,该图片将作为缓冲区
        /// </summary>
        Bitmap cacheBitmap = null;

        /// <summary>
        /// 定义cacheBitmap上的Canvas对象
        /// </summary>
        Canvas cacheCanvas = null;


        /// <summary>
        /// 用于代码创建控件的构造函数
        /// </summary>
        /// <param name="c"></param>
        public HandDrawView(Context c) : base(c)
        {
            // 创建一个与该View相同大小的缓冲区
            cacheBitmap = Bitmap.CreateBitmap(VIEW_WIDTH, VIEW_HEIGHT, Bitmap.Config.Argb8888);
            cacheCanvas = new Canvas();
            path = new Path();
            // 设置cacheCanvas将会绘制到内存中的cacheBitmap上
            cacheCanvas.SetBitmap(cacheBitmap);
            // 设置画笔的颜色
            paint = new Paint(PaintFlags.Dither);
            paint.Color = Color.Red;
            // 设置画笔风格
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = 1;      // 设置笔画宽度
            // 反锯齿
            paint.AntiAlias = true;
            paint.Dither = true;
        }



        /// <summary>
        /// 用于在 layout目录的布局文件中注册控件的构造方法
        /// </summary>
        /// <param name="c">用于显示此控件的活动的当前上下文环境</param>
        /// <param name="set">布局文件中控件设置的属性</param>
        public HandDrawView(Context c, IAttributeSet set) : base(c, set)
        {
            // 创建一个与该View相同大小的缓冲区
            cacheBitmap = Bitmap.CreateBitmap(VIEW_WIDTH, VIEW_HEIGHT, Bitmap.Config.Argb8888);
            cacheCanvas = new Canvas();
            path = new Path();
            // 设置cacheCanvas将会绘制到内存中的cacheBitmap上
            cacheCanvas.SetBitmap(cacheBitmap);
            // 设置画笔的颜色
            paint = new Paint(PaintFlags.Dither);
            paint.Color = Color.Red;
            // 设置画笔风格
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = 1;      // 设置笔画宽度
            // 反锯齿
            paint.AntiAlias = true;
            paint.Dither = true;
        }


        public HandDrawView(Context context, int width, int height) : base(context)
        {
            // 创建一个与该View相同大小的缓存区
            cacheBitmap = Bitmap.CreateBitmap(width, height,
                Bitmap.Config.Argb8888);
            cacheCanvas = new Canvas();
            path = new Path();
            // 设置cacheCanvas将会绘制到内存中的cacheBitmap上
            cacheCanvas.SetBitmap(cacheBitmap);
            // 设置画笔的颜色
            paint = new Paint(PaintFlags.Dither);
            paint.Color = Color.Red;
            // 设置画笔风格
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = 1;
            // 反锯齿
            paint.AntiAlias = true;
            paint.Dither = true;
        }


        /// <summary>
        /// 触摸屏幕的事件
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override bool OnTouchEvent(MotionEvent e)
        {
            // 获取拖动事件的发生位置
            float x = e.GetX();
            float y = e.GetY();
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    path.MoveTo(x, y);
                    preX = x;
                    preY = y;
                    break;
                case MotionEventActions.Move:
                    // 添加一个二次贝塞尔曲线的最后一点,接近控制点(x1, y1),到最后(x2, y2)。
                    path.QuadTo(preX, preY, x, y);
                    preX = x;
                    preY = y;
                    break;
                case MotionEventActions.Up:
                    // 调用了缓存Bitmap的Canvas进行绘图
                    cacheCanvas.DrawPath(path, paint);
                    path.Reset();
                    break;
                default:
                    break;
            }
            Invalidate();
            // 返回true表示处理方法已经出来该事件
            return true;
        }

        /// <summary>
        /// 此方法在Java项目中为public
        /// </summary>
        /// <param name="canvas"></param>
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            // 设置画布的颜色
            canvas.DrawColor(Color.White);
            Paint bmpPaint = new Paint();
            // 将缓存中的cacheBitmap绘制到该View组件上
            canvas.DrawBitmap(cacheBitmap, 0, 0, bmpPaint);
            // 沿着path绘制
            canvas.DrawPath(path, paint);
        }

    }
}
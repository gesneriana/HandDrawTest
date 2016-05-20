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
    /// ʵ��һ����ͼ��,����ָ�ڴ��������ƶ�ʱ,��������Ļ�ϻ��������ͼ��
    /// </summary>
    public class HandDrawView : View
    {
        /// <summary>
        /// ��¼��ǰ��x����
        /// </summary>
        float preX;

        /// <summary>
        /// ��¼��ǰ��y����
        /// </summary>
        float preY;

        /// <summary>
        /// �Զ��廭ͼ��Ļ���·������
        /// </summary>
        private Path path;

        /// <summary>
        /// �Զ��廭ͼ��Ļ��ʶ���
        /// </summary>
        public Paint paint = null;


        const int VIEW_WIDTH = 320;
        const int VIEW_HEIGHT = 480;

        /// <summary>
        /// ����һ���ڴ��е�ͼƬ,��ͼƬ����Ϊ������
        /// </summary>
        Bitmap cacheBitmap = null;

        /// <summary>
        /// ����cacheBitmap�ϵ�Canvas����
        /// </summary>
        Canvas cacheCanvas = null;


        /// <summary>
        /// ���ڴ��봴���ؼ��Ĺ��캯��
        /// </summary>
        /// <param name="c"></param>
        public HandDrawView(Context c) : base(c)
        {
            // ����һ�����View��ͬ��С�Ļ�����
            cacheBitmap = Bitmap.CreateBitmap(VIEW_WIDTH, VIEW_HEIGHT, Bitmap.Config.Argb8888);
            cacheCanvas = new Canvas();
            path = new Path();
            // ����cacheCanvas������Ƶ��ڴ��е�cacheBitmap��
            cacheCanvas.SetBitmap(cacheBitmap);
            // ���û��ʵ���ɫ
            paint = new Paint(PaintFlags.Dither);
            paint.Color = Color.Red;
            // ���û��ʷ��
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = 1;      // ���ñʻ����
            // �����
            paint.AntiAlias = true;
            paint.Dither = true;
        }



        /// <summary>
        /// ������ layoutĿ¼�Ĳ����ļ���ע��ؼ��Ĺ��췽��
        /// </summary>
        /// <param name="c">������ʾ�˿ؼ��Ļ�ĵ�ǰ�����Ļ���</param>
        /// <param name="set">�����ļ��пؼ����õ�����</param>
        public HandDrawView(Context c, IAttributeSet set) : base(c, set)
        {
            // ����һ�����View��ͬ��С�Ļ�����
            cacheBitmap = Bitmap.CreateBitmap(VIEW_WIDTH, VIEW_HEIGHT, Bitmap.Config.Argb8888);
            cacheCanvas = new Canvas();
            path = new Path();
            // ����cacheCanvas������Ƶ��ڴ��е�cacheBitmap��
            cacheCanvas.SetBitmap(cacheBitmap);
            // ���û��ʵ���ɫ
            paint = new Paint(PaintFlags.Dither);
            paint.Color = Color.Red;
            // ���û��ʷ��
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = 1;      // ���ñʻ����
            // �����
            paint.AntiAlias = true;
            paint.Dither = true;
        }


        public HandDrawView(Context context, int width, int height) : base(context)
        {
            // ����һ�����View��ͬ��С�Ļ�����
            cacheBitmap = Bitmap.CreateBitmap(width, height,
                Bitmap.Config.Argb8888);
            cacheCanvas = new Canvas();
            path = new Path();
            // ����cacheCanvas������Ƶ��ڴ��е�cacheBitmap��
            cacheCanvas.SetBitmap(cacheBitmap);
            // ���û��ʵ���ɫ
            paint = new Paint(PaintFlags.Dither);
            paint.Color = Color.Red;
            // ���û��ʷ��
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = 1;
            // �����
            paint.AntiAlias = true;
            paint.Dither = true;
        }


        /// <summary>
        /// ������Ļ���¼�
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override bool OnTouchEvent(MotionEvent e)
        {
            // ��ȡ�϶��¼��ķ���λ��
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
                    // ���һ�����α��������ߵ����һ��,�ӽ����Ƶ�(x1, y1),�����(x2, y2)��
                    path.QuadTo(preX, preY, x, y);
                    preX = x;
                    preY = y;
                    break;
                case MotionEventActions.Up:
                    // �����˻���Bitmap��Canvas���л�ͼ
                    cacheCanvas.DrawPath(path, paint);
                    path.Reset();
                    break;
                default:
                    break;
            }
            Invalidate();
            // ����true��ʾ�������Ѿ��������¼�
            return true;
        }

        /// <summary>
        /// �˷�����Java��Ŀ��Ϊpublic
        /// </summary>
        /// <param name="canvas"></param>
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            // ���û�������ɫ
            canvas.DrawColor(Color.White);
            Paint bmpPaint = new Paint();
            // �������е�cacheBitmap���Ƶ���View�����
            canvas.DrawBitmap(cacheBitmap, 0, 0, bmpPaint);
            // ����path����
            canvas.DrawPath(path, paint);
        }

    }
}
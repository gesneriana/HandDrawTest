using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Util;

namespace HandDrawTest
{
    [Activity(Label = "HandDrawTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        BlurMaskFilter blur;

        EmbossMaskFilter emboss;

        HandDrawView hdv;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            DisplayMetrics displayMetrics = new DisplayMetrics();
            // 获取创建的宽度和高度
            WindowManager.DefaultDisplay.GetRealMetrics(displayMetrics);
            // 创建一个DrawView，该DrawView的宽度、高度与该Activity保持相同
            hdv = new HandDrawView(this, displayMetrics.WidthPixels, displayMetrics.HeightPixels);
            emboss = new EmbossMaskFilter(new float[] { 1.5f, 1.5f, 1.5f }, 0.6f, 6, 4.2f);
            blur = new BlurMaskFilter(8, BlurMaskFilter.Blur.Normal);
        }

        /// <summary>
        /// 重写父类的菜单按钮创建
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater inflator = new MenuInflater(this);
            // 状态R.menu.context对应的菜单,并添加到menu中
            inflator.Inflate(Resource.Menu.my_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>
        /// 菜单项被选中后的回调方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // 根据自定义控件的ID获取控件的实例
            HandDrawView hdv = (HandDrawView)FindViewById(Resource.Id.hand_draw_view);
            // 判断单击的是哪个菜单项,并有针对性地做出响应
            switch (item.ItemId)
            {
                case Resource.Id.red:
                    hdv.paint.Color = Color.Red;
                    item.SetChecked(true);
                    break;
                case Resource.Id.green:
                    hdv.paint.Color = Color.Green;
                    item.SetChecked(true);
                    break;
                case Resource.Id.blue:
                    hdv.paint.Color = Color.Blue;
                    item.SetChecked(true);
                    break;
                case Resource.Id.width_1:
                    hdv.paint.StrokeWidth = 1;
                    break;
                case Resource.Id.width_3:
                    hdv.paint.StrokeWidth = 3;
                    break;
                case Resource.Id.width_5:
                    hdv.paint.StrokeWidth = 5;
                    break;
                case Resource.Id.blur:
                    hdv.paint.SetMaskFilter(blur);
                    break;
                case Resource.Id.emboss:
                    hdv.paint.SetMaskFilter(emboss);
                    break;
                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}


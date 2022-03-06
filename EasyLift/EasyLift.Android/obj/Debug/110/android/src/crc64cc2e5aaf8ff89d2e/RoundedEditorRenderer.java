package crc64cc2e5aaf8ff89d2e;


public class RoundedEditorRenderer
	extends crc643f46942d9dd1fff9.EditorRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("EasyLift.Droid.RoundedEditorRenderer, EasyLift.Android", RoundedEditorRenderer.class, __md_methods);
	}


	public RoundedEditorRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == RoundedEditorRenderer.class)
			mono.android.TypeManager.Activate ("EasyLift.Droid.RoundedEditorRenderer, EasyLift.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public RoundedEditorRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == RoundedEditorRenderer.class)
			mono.android.TypeManager.Activate ("EasyLift.Droid.RoundedEditorRenderer, EasyLift.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public RoundedEditorRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == RoundedEditorRenderer.class)
			mono.android.TypeManager.Activate ("EasyLift.Droid.RoundedEditorRenderer, EasyLift.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

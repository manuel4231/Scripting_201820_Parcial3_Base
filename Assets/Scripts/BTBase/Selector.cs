public class Selector : Composite
{
    protected override bool MustAllChildrenSucceed
    {
        get
        {
            return false;
        }
    }

    public override bool Execute()
    {
        bool result = CheckCondition();

        if (result)
        {
            result = base.Execute();
        }

        return result;
    }

    protected override bool ShouldBreak(bool result)
    {
        return result;
    }

    protected virtual bool CheckCondition()
    {
        return true;
    }
}
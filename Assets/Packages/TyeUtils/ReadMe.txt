using TyeUtils;

#TweenUtils

    Initialise Instance of tweenUtils:
        TweenUtils tweenUtils = new();

    Add Callback Method (Optional):
        tweenUtils.TweenFinished += MyCallbackMethod;

    Start Tween:
        tweenUtils.StartVector3Tween(this, [<Vector3> startVector], [<Vector3> endVector], [VectorUpdateMethod], [-OPTIONAL-<float> duration], [-OPTIONAL-<AnimationCurve> curve]);
        tweenUtils.StartFloatTween(this, [<float> startFloat], [<float> endFloat], [floatUpdateMethod], [-OPTIONAL-<float> duration], [-OPTIONAL-<AnimationCurve> curve]);
        tweenUtils.StartPositionTween(this, [<float> startFloat], [<float> endFloat], [floatUpdateMethod], [<float> diameter] [-OPTIONAL-<float> duration], [-OPTIONAL-<AnimationCurve> curve]);

    UpdateMethods:
        You could either create a Function to handle it:
            public void UpdateVector(Vector3 val)
            {
                //Do something with val
            }
            public void UpdateFloat(float val)
            {
                //Do something with val
            }
        or you could create a lambda expression in place of the square brackets:
            , val => {//Do something with val},

    Functions:
        tweenUtils.StopTween();

    Vars (Optional):
        tweenUtils.IsTweening           [bool]
        tweenUtils.TweenProgress        [float <0-1>]


    -
#Something Else


    -
using UnityEngine;

public class ViewField : BaseField
{
    private DmgDone dmgDone;
    public bool isMini = false;
    public ViewFieldComponent viewFieldComponent;
	public ViewAnimationComponent viewAnimationComponent;

    public ViewField() :base() {
        warship = null;
    }

    public void SetViewFieldComponent(ViewFieldComponent component)
    {
        this.viewFieldComponent = component;
    }

	public void SetViewAnimationComponent(ViewAnimationComponent animationComponent){
		this.viewAnimationComponent = animationComponent;
	}

    public void SetWarshipColor() {
        viewFieldComponent.SetWarshipColor();
    }

	public void SetEffect(){
		viewAnimationComponent.SetEffect (Variables.animationTriggerHit);
		viewAnimationComponent.SetEffect (Variables.animationTriggerBack);
	}

    public void SetShotResult(DmgDone result) {
        dmgDone = result;
    }

    public bool IsPressed() {
        return viewFieldComponent.IsPressed();
    }

    public void SetColorOnField(DmgDone shotResult)
    {
        viewFieldComponent.SetColorOnField(shotResult);
    }
		
    public void SetEffectOnField(DmgDone shotResult) {
        viewAnimationComponent.SetEffectOnField(shotResult);       
    }

}

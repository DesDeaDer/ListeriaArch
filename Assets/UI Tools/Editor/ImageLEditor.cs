using System.Linq;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UITools {
  /// <summary>
  /// Editor class used to edit UI Sprites.
  /// </summary>

  [CustomEditor(typeof(ImageL), true)]
  [CanEditMultipleObjects]
  public class ImageLEditor : GraphicEditor {
    SerializedProperty m_FillMethod;
    SerializedProperty m_FillOrigin;
    SerializedProperty m_FillAmount;
    SerializedProperty m_FillClockwise;
    SerializedProperty m_Type;
    SerializedProperty m_FillCenter;
    SerializedProperty m_Sprite;
    SerializedProperty m_PreserveAspect;
    GUIContent m_SpriteContent;
    GUIContent m_SpriteTypeContent;
    GUIContent m_ClockwiseContent;
    AnimBool m_ShowSlicedOrTiled;
    AnimBool m_ShowSliced;
    AnimBool m_ShowFilled;
    AnimBool m_ShowType;

    protected override void OnEnable() {
      base.OnEnable();

      m_SpriteContent = new GUIContent("Source Image");
      m_SpriteTypeContent = new GUIContent("Image Type");
      m_ClockwiseContent = new GUIContent("Clockwise");

      m_Sprite = serializedObject.FindProperty("m_Sprite");
      m_Type = serializedObject.FindProperty("m_Type");
      m_FillCenter = serializedObject.FindProperty("m_FillCenter");
      m_FillMethod = serializedObject.FindProperty("m_FillMethod");
      m_FillOrigin = serializedObject.FindProperty("m_FillOrigin");
      m_FillClockwise = serializedObject.FindProperty("m_FillClockwise");
      m_FillAmount = serializedObject.FindProperty("m_FillAmount");
      m_PreserveAspect = serializedObject.FindProperty("m_PreserveAspect");

      m_ShowType = new AnimBool(m_Sprite.objectReferenceValue != null);
      m_ShowType.valueChanged.AddListener(Repaint);

      var typeEnum = (ImageL.Type)m_Type.enumValueIndex;

      m_ShowSlicedOrTiled = new AnimBool(!m_Type.hasMultipleDifferentValues && typeEnum == ImageL.Type.Sliced);
      m_ShowSliced = new AnimBool(!m_Type.hasMultipleDifferentValues && typeEnum == ImageL.Type.Sliced);
      m_ShowFilled = new AnimBool(!m_Type.hasMultipleDifferentValues && typeEnum == ImageL.Type.Filled);
      m_ShowSlicedOrTiled.valueChanged.AddListener(Repaint);
      m_ShowSliced.valueChanged.AddListener(Repaint);
      m_ShowFilled.valueChanged.AddListener(Repaint);

      SetShowNativeSize(true);
    }

    protected override void OnDisable() {
      m_ShowType.valueChanged.RemoveListener(Repaint);
      m_ShowSlicedOrTiled.valueChanged.RemoveListener(Repaint);
      m_ShowSliced.valueChanged.RemoveListener(Repaint);
      m_ShowFilled.valueChanged.RemoveListener(Repaint);
    }

    public override void OnInspectorGUI() {
      serializedObject.Update();

      SpriteGUI();
      AppearanceControlsGUI();

      m_ShowType.target = m_Sprite.objectReferenceValue != null;
      if (EditorGUILayout.BeginFadeGroup(m_ShowType.faded))
        TypeGUI();
      EditorGUILayout.EndFadeGroup();

      SetShowNativeSize(false);
      if (EditorGUILayout.BeginFadeGroup(m_ShowNativeSize.faded)) {
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(m_PreserveAspect);
        EditorGUI.indentLevel--;
      }
      EditorGUILayout.EndFadeGroup();
      NativeSizeButtonGUI();

      serializedObject.ApplyModifiedProperties();
    }

    void SetShowNativeSize(bool instant) {
      ImageL.Type type = (ImageL.Type)m_Type.enumValueIndex;
      bool showNativeSize = (type == ImageL.Type.Simple || type == ImageL.Type.Filled);
      base.SetShowNativeSize(showNativeSize, instant);
    }

    /// <summary>
    /// Draw the atlas and Image selection fields.
    /// </summary>

    protected void SpriteGUI() {
      EditorGUI.BeginChangeCheck();
      EditorGUILayout.PropertyField(m_Sprite, m_SpriteContent);
      if (EditorGUI.EndChangeCheck()) {
        var newSprite = m_Sprite.objectReferenceValue as Sprite;
        if (newSprite) {
          ImageL.Type oldType = (ImageL.Type)m_Type.enumValueIndex;
          if (newSprite.border.SqrMagnitude() > 0) {
            m_Type.enumValueIndex = (int)ImageL.Type.Sliced;
          } else if (oldType == ImageL.Type.Sliced) {
            m_Type.enumValueIndex = (int)ImageL.Type.Simple;
          }
        }
      }
    }

    /// <summary>
    /// Sprites's custom properties based on the type.
    /// </summary>

    protected void TypeGUI() {
      EditorGUILayout.PropertyField(m_Type, m_SpriteTypeContent);

      ++EditorGUI.indentLevel;
      {
        ImageL.Type typeEnum = (ImageL.Type)m_Type.enumValueIndex;

        bool showSlicedOrTiled = (!m_Type.hasMultipleDifferentValues && (typeEnum == ImageL.Type.Sliced || typeEnum == ImageL.Type.Tiled));
        if (showSlicedOrTiled && targets.Length > 1)
          showSlicedOrTiled = targets.Select(obj => obj as ImageL).All(img => img.hasBorder);

        m_ShowSlicedOrTiled.target = showSlicedOrTiled;
        m_ShowSliced.target = (showSlicedOrTiled && !m_Type.hasMultipleDifferentValues && typeEnum == ImageL.Type.Sliced);
        m_ShowFilled.target = (!m_Type.hasMultipleDifferentValues && typeEnum == ImageL.Type.Filled);

        ImageL image = target as ImageL;
        if (EditorGUILayout.BeginFadeGroup(m_ShowSlicedOrTiled.faded)) {
          if (image.hasBorder)
            EditorGUILayout.PropertyField(m_FillCenter);
        }
        EditorGUILayout.EndFadeGroup();

        if (EditorGUILayout.BeginFadeGroup(m_ShowSliced.faded)) {
          if (image.sprite != null && !image.hasBorder)
            EditorGUILayout.HelpBox("This Image doesn't have a border.", MessageType.Warning);
        }
        EditorGUILayout.EndFadeGroup();

        if (EditorGUILayout.BeginFadeGroup(m_ShowFilled.faded)) {
          EditorGUI.BeginChangeCheck();
          EditorGUILayout.PropertyField(m_FillMethod);
          if (EditorGUI.EndChangeCheck()) {
            m_FillOrigin.intValue = 0;
          }
          switch ((ImageL.FillMethod)m_FillMethod.enumValueIndex) {
            case ImageL.FillMethod.Horizontal:
              m_FillOrigin.intValue = (int)(ImageL.OriginHorizontal)EditorGUILayout.EnumPopup("Fill Origin", (Image.OriginHorizontal)m_FillOrigin.intValue);
              break;
            case ImageL.FillMethod.Vertical:
              m_FillOrigin.intValue = (int)(ImageL.OriginVertical)EditorGUILayout.EnumPopup("Fill Origin", (Image.OriginVertical)m_FillOrigin.intValue);
              break;
            case ImageL.FillMethod.Radial90:
              m_FillOrigin.intValue = (int)(ImageL.Origin90)EditorGUILayout.EnumPopup("Fill Origin", (Image.Origin90)m_FillOrigin.intValue);
              break;
            case ImageL.FillMethod.Radial180:
              m_FillOrigin.intValue = (int)(ImageL.Origin180)EditorGUILayout.EnumPopup("Fill Origin", (Image.Origin180)m_FillOrigin.intValue);
              break;
            case ImageL.FillMethod.Radial360:
              m_FillOrigin.intValue = (int)(ImageL.Origin360)EditorGUILayout.EnumPopup("Fill Origin", (Image.Origin360)m_FillOrigin.intValue);
              break;
          }
          EditorGUILayout.PropertyField(m_FillAmount);
          if ((ImageL.FillMethod)m_FillMethod.enumValueIndex > ImageL.FillMethod.Vertical) {
            EditorGUILayout.PropertyField(m_FillClockwise, m_ClockwiseContent);
          }
        }
        EditorGUILayout.EndFadeGroup();
      }
      --EditorGUI.indentLevel;
    }

    /// <summary>
    /// All graphics have a preview.
    /// </summary>

    public override bool HasPreviewGUI() { return true; }

    /// <summary>
    /// Draw the Image preview.
    /// </summary>

    public override void OnPreviewGUI(Rect rect, GUIStyle background) {
      ImageL image = target as ImageL;
      if (image == null) return;

      Sprite sf = image.sprite;
      if (sf == null) return;

      SpriteDrawUtility.DrawSprite(sf, rect, image.canvasRenderer.GetColor());
    }

    /// <summary>
    /// Info String drawn at the bottom of the Preview
    /// </summary>

    public override string GetInfoString() {
      ImageL image = target as ImageL;
      Sprite sprite = image.sprite;

      int x = (sprite != null) ? Mathf.RoundToInt(sprite.rect.width) : 0;
      int y = (sprite != null) ? Mathf.RoundToInt(sprite.rect.height) : 0;

      return string.Format("Image Size: {0}x{1}", x, y);
    }

    public static class SpriteDrawUtility {
      delegate void DDrawSprite(Sprite sprite, Rect drawArea, Color color);
      public static void DrawSprite(Sprite sprite, Rect drawArea, Color color) =>
        typeof(SliderEditor)
        .GetMethod<DDrawSprite>(nameof(SpriteDrawUtility), DrawSprite)
        (sprite, drawArea, color);
    }
  }
}

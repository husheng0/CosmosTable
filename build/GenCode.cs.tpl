using CosmosTable;

namespace {{ NameSpace }}
{
{% for file in Files %}
	/// <summary>
	/// Auto Generate for Tab File: {{ file.TabFilePath }}
	/// </summary>
	public partial class {{file.ClassName}}Info : TableRowInfo
	{
		public static readonly string TabFilePath = "{{ file.TabFilePath }}";
		
		public override bool IsAutoParse { get { return false; } }

		{% for field in file.Fields %}
		public {{ field.Type }} {{ field.Name}} { get; internal set; }  // {{ field.Comment }}
		{% endfor %}

		public override void Parse(string[] values)
		{
		{% for field in file.Fields %}
			// {{ field.Comment }}
			{{ field.Name}} = Get_{{ field.Type | replace:'\[\]','_array' }}(values[{{ field.Index }}], "{{ field.DefaultValue }}");
		{% endfor %}
		}

		public override object PrimaryKey
		{
			get
			{
				return {{ file.PrimaryKey }};
			}
		}
	}
{% endfor %}
}
